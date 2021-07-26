using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Repository
{
    public class BatchRepository : IBatchRepository
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<BatchRepository> _logger;

        public BatchRepository(IApplicationContext context, ILogger<BatchRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddNumberToBatch(Number newNumber)
        {
            var batch = _context.Batches.Find(newNumber.BatchId);
            batch.Numbers.Add(newNumber);

            ((DbContext)_context).Update(batch);
            await ((DbContext)_context).SaveChangesAsync();

            _logger.LogInformation("Number added to batch. Batch Id: {batchId}, Number Id: {numberId}",
                batch.Id,
                newNumber.Id);
        }

        public async Task<Batch> CreateBatch(Batch newBatch)
        {
            await _context.Batches.AddAsync(newBatch);

            await ((DbContext)_context).SaveChangesAsync();

            _logger.LogInformation("Batch created with id: {batchId}",
                newBatch.Id);

            return newBatch;
        }

        public async Task<Batch> GetBatch(Guid batchId)
        {
            return await _context.Batches
                .Include(x => x.Numbers)
                .SingleAsync(x => x.Id == batchId);
        }

        public void UpdateBatch(Batch batch)
        {
            lock(_context.Lock)
            {
                _context.Batches.Update(batch);
                ((DbContext)_context).SaveChanges();

                _logger.LogInformation("Batch {batchId} updated.", batch.Id);
            }
        }

        public async Task UpdateBatchAsync(Batch batch)
        {
            _context.Batches.Update(batch);
            await ((DbContext)_context).SaveChangesAsync();

            _logger.LogInformation("Batch {batchId} updated.", batch.Id);
        }
    }
}
