using BatchProcessor.ProcessorApi.Entities;
using BatchProcessor.ProcessorApi.Interfaces.Factories;
using BatchProcessor.ProcessorApi.Interfaces.Repository;
using BatchProcessor.ProcessorApi.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BatchProcessor.ProcessorApi.Services
{
    public class BatchService : IBatchService
    {
        private readonly IBatchFactory _batchFactory;
        private readonly IBatchRepository _batchRepository;

        public BatchService(
            IBatchFactory batchFactory,
            IBatchRepository batchRepository)
        {
            _batchFactory = batchFactory ?? throw new ArgumentNullException(nameof(batchFactory));
            _batchRepository = batchRepository ?? throw new ArgumentNullException(nameof(batchRepository));
        }

        public async IAsyncEnumerable<Batch> CreateBatch(Guid processId, int numberOfBatches)
        {
            foreach (var item in Enumerable.Range(0, numberOfBatches))
            {
                var newBatch = _batchFactory
                .SetOrder(item)
                .SetProcessId(processId)
                .Build();

                yield return await _batchRepository.CreateBatch(newBatch);
            }
        }
    }
}
