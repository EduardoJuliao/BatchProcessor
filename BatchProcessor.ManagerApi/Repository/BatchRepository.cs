using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Repository
{
    public class BatchRepository : IBatchRepository
    {
        private readonly IApplicationContext _context;

        public BatchRepository(IApplicationContext context)
        {
            _context = context;
        }

        public async Task AddNumberToBatch(Number newNumber)
        {
            var batch = _context.Batches.Find(newNumber.BatchId);
            batch.Numbers.Add(newNumber);

            ((DbContext)_context).Update(batch);
            await ((DbContext)_context).SaveChangesAsync();

        }

        public async Task<Batch> CreateBatch(Batch newBatch)
        {
            await _context.Batches.AddAsync(newBatch);

            await ((DbContext)_context).SaveChangesAsync();

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
            }
        }

        public async Task UpdateBatchAsync(Batch batch)
        {
            _context.Batches.Update(batch);
            await ((DbContext)_context).SaveChangesAsync();
        }
    }
}
