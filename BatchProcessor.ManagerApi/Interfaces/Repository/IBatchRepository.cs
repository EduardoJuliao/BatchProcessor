using BatchProcessor.ManagerApi.Entities;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Interfaces.Repository
{
    public interface IBatchRepository
    {
        Task<Batch> AddNumberToBatch(Number newNumber);

        Task<Batch> AddNumberToBatch(Guid batchId, Number newNumber);

        Task<Batch> CreateBatch(Batch newBatch);

        Task<Batch> GetBatch(Guid batchId);
    }
}