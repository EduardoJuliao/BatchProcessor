using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Factories;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Services
{
    public class BatchService : IBatchService
    {
        private readonly IBatchFactory _batchFactory;
        private readonly IBatchRepository _batchRepository;
        private readonly ILogger<BatchService> _logger;

        public BatchService(
            IBatchFactory batchFactory,
            IBatchRepository batchRepository,
            ILogger<BatchService> logger)
        {
            _batchFactory = batchFactory ?? throw new ArgumentNullException(nameof(batchFactory));
            _batchRepository = batchRepository ?? throw new ArgumentNullException(nameof(batchRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async IAsyncEnumerable<Batch> CreateBatches(Guid processId, int numberOfBatches)
        {
            _logger.LogInformation("Creating {numberOfBatches} amount of batches for process {processId}", numberOfBatches, processId);
            foreach (var item in Enumerable.Range(0, numberOfBatches))
            {
                var newBatch = _batchFactory
                .SetOrder(item)
                .SetProcessId(processId)
                .Build();

                _logger.LogInformation("New batch created with id {batchId} for process {processId}", newBatch.Id, processId);

                yield return await _batchRepository.CreateBatch(newBatch);
            }
        }

        public async Task<Batch> GetStatus(Guid batchId)
        {
            return await _batchRepository.GetBatch(batchId);
        }
    }
}
