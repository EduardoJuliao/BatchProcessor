using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Events.Data;
using BatchProcessor.ManagerApi.Interfaces.Factories;
using BatchProcessor.ManagerApi.Interfaces.Managers;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BatchProcessor.ManagerApi.Managers
{
    public class NumberManager : INumberManager
    {
        private readonly HttpOptions _options;
        private readonly IBatchRepository _batchRepository;
        private readonly IMultiplyManager _multiplyManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NumberManager> _logger;

        public event EventHandler<NumberGeneratedEventData> OnNumberGenerated;
        public event EventHandler<NumberMultipliedEventData> OnNumberMultiplied;

        public NumberManager(
            HttpOptions options,
            IBatchRepository batchRepository,
            IMultiplyManager multiplyManager,
            IServiceProvider serviceProvider,
            ILogger<NumberManager> logger)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _batchRepository = batchRepository ?? throw new ArgumentNullException(nameof(batchRepository));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _multiplyManager = multiplyManager ?? throw new ArgumentNullException(nameof(multiplyManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Batch> Generate(Batch batch)
        {
            _logger.LogInformation("Starting number generation for batch {batchId}", batch.Id);
            _multiplyManager.OnNumberMultiplied += OnNumberMultiplied;

            if (batch.Numbers == null)
                batch.Numbers = new List<Number>();

            using var client = new WebClient();

            var _ = await client.OpenReadTaskAsync(new Uri($"{_options.BaseUrl}/{_options.NumberGeneratorEndpoint}/{batch.Size}"))
                .ConfigureAwait(true);

            using var reader = new StreamReader(_);
            var order = 0;
            while (!reader.EndOfStream)
            {
                try
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line) && line.StartsWith("generated_number: "))
                    {

                        order++;
                        var value = Convert.ToInt32(line.Split(" ")[1]);

                        _logger.LogInformation("Number {newNumber} in order {order} generated for batch {batchId}", value, order, batch.Id);

                        var newNumber = _serviceProvider.GetService<INumberFactory>()
                            .SetOrder(order)
                            .SetOriginalValue(value)
                            .Build();

                        batch.Numbers.Add(newNumber);

                        _batchRepository.UpdateBatch(batch);

                        _logger.LogInformation("Updated batch {batchId} with number generated with id {numberId}", batch.Id, newNumber.Id);

                        OnNumberGenerated?.Invoke(this, new NumberGeneratedEventData { Number = newNumber });

                        _logger.LogInformation("Starting the process for multiply the number {numberValue} in batch {batchId}", newNumber.Value, batch.Id);
                        await _multiplyManager.Multiply(newNumber);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "an error occured while reading responses from the api.");
                }
            }

            return batch;
        }

        public async Task Generate(Process process)
        {
            await Task.Run(() =>  Parallel.ForEach(process.Batches, (batch) => Generate(batch).ConfigureAwait(false).GetAwaiter().GetResult()));
        }
    }
}
