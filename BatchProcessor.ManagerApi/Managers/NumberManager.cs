using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Interfaces.Factories;
using BatchProcessor.ManagerApi.Interfaces.Managers;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Options;
using BatchProcessor.ManagerApi.Repository;

namespace BatchProcessor.ManagerApi.Managers
{
    public class NumberManager : INumberManager
    {
        private readonly HttpClient _httpClient;
        private readonly HttpOptions _options;
        private readonly IBatchRepository _batchRepository;
        private readonly INumberFactory _numberfactory;
        private readonly IMultiplyManager _multiplyManager;

        public NumberManager(
            HttpOptions options,
            IBatchRepository batchRepository,
            INumberFactory numberfactory,
            IMultiplyManager multiplyManager)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(options.BaseUrl)
            };
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _batchRepository = batchRepository ?? throw new ArgumentNullException(nameof(batchRepository));
            _numberfactory = numberfactory ?? throw new ArgumentNullException(nameof(numberfactory));
            _multiplyManager = multiplyManager ?? throw new ArgumentNullException(nameof(multiplyManager));
        }

        public async Task Generate(Guid batchId)
        {
            var batch = await _batchRepository.GetBatch(batchId);

            Parallel.For(0, batch.Size, async (order) =>
            {
                var response = await _httpClient.GetAsync(_options.NumberGeneratorEndpoint);
                var json = await response.Content.ReadAsStringAsync();
                var value = JsonSerializer.Deserialize<int>(json);

                var newNumber = _numberfactory
                    .SetOrder(order)
                    .SetOriginalValue(value)
                    .SetBatchId(batchId)
                    .Build();

                await _batchRepository.AddNumberToBatch(newNumber);

                new Task(() => _multiplyManager.Multiply(newNumber.Id));
            });
        }
    }
}
