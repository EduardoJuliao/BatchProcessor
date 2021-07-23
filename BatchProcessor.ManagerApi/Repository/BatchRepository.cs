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

        public async Task<Batch> AddNumberToBatch(Guid batchId, Number newNumber)
        {
            var batch = await _context.Batches.FindAsync(batchId);
            batch.Numbers.Add(newNumber);

            await ((DbContext)_context).SaveChangesAsync();

            return batch;
        }

        public async Task<Batch> AddNumberToBatch(Number newNumber)
        {
            var batch = await _context.Batches.FindAsync(newNumber.BatchId);
            batch.Numbers.Add(newNumber);

            await ((DbContext)_context).SaveChangesAsync();

            return batch;
        }

        public async Task<Batch> CreateBatch(Batch newBatch)
        {
            await _context.Batches.AddAsync(newBatch);

            await ((DbContext)_context).SaveChangesAsync();

            return newBatch;
        }
    }
}
