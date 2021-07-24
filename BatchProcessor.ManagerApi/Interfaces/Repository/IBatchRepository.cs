using BatchProcessor.ManagerApi.Entities;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Interfaces.Repository
{
    public interface IBatchRepository
    {
        Task AddNumberToBatch(Number newNumber);

        Task<Batch> CreateBatch(Batch newBatch);

        Task<Batch> GetBatch(Guid batchId);

        void UpdateBatch(Batch batch);
        Task UpdateBatchAsync(Batch batch);
    }
}