using BatchProcessor.ProcessorApi.Entities;
using BatchProcessor.ProcessorApi.Interfaces.Factories;
using BatchProcessor.ProcessorApi.Interfaces.Repository;
using BatchProcessor.ProcessorApi.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BatchProcessor.ProcessorApi.Services
{
    public class NumberGeneratorService : INumberGeneratorService
    {
        private static readonly Random _random = new Random();

        private readonly IWorkerService _processorService;
        private readonly IBatchRepository _batchRepository;
        private readonly INumberFactory _numberFactory;

        public NumberGeneratorService(IWorkerService processorService, IBatchRepository batchRepository, INumberFactory numberFactory)
        {
            _processorService = processorService ?? throw new ArgumentNullException(nameof(processorService));
            _batchRepository = batchRepository ?? throw new ArgumentNullException(nameof(batchRepository));
            _numberFactory = numberFactory ?? throw new ArgumentNullException(nameof(numberFactory));
        }

        public async IAsyncEnumerable<Number> Generate(Guid batchId, int amount)
        {
            foreach(var number in Enumerable.Range(0, amount).Select(x => new { order = x, value = _random.Next(0, 100) }))
            {
                await _processorService.Process();

                var newNumber = _numberFactory
                    .SetOrder(number.order)
                    .SetValue(number.value)
                    .SetBatchId(batchId)
                    .Build();

                await _batchRepository.AddNumberToBatch(newNumber);

                yield return newNumber;
            }
        }
    }
}
