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

namespace BatchProcessor.ManagerApi.Managers
{
    public class NumberManager : INumberManager
    {
        private readonly HttpOptions _options;
        private readonly IBatchRepository _batchRepository;
        private readonly IMultiplyManager _multiplyManager;
        private readonly IServiceProvider _serviceProvider;

        public event EventHandler<NumberGeneratedEventData> OnNumberGenerated;
        public event EventHandler<NumberMultipliedEventData> OnNumberMultiplied;

        public NumberManager(
            HttpOptions options,
            IBatchRepository batchRepository,
            IMultiplyManager multiplyManager,
            IServiceProvider serviceProvider)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _batchRepository = batchRepository ?? throw new ArgumentNullException(nameof(batchRepository));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _multiplyManager = multiplyManager ?? throw new ArgumentNullException(nameof(multiplyManager));

        }

        public async Task<Batch> Generate(Batch batch)
        {
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
                var line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line) && line.StartsWith("generated_number: "))
                {
                    order++;
                    var value = Convert.ToInt32(line.Split(" ")[1]);

                    var newNumber = _serviceProvider.GetService<INumberFactory>()
                        .SetOrder(order)
                        .SetOriginalValue(value)
                        .Build();

                    batch.Numbers.Add(newNumber);

                    _batchRepository.UpdateBatch(batch);

                     OnNumberGenerated?.Invoke(this, new NumberGeneratedEventData { Number = newNumber });

                    await _multiplyManager.Multiply(newNumber);
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
