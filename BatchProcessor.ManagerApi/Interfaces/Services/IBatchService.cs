using BatchProcessor.ManagerApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Interfaces.Services
{
    public interface IBatchService
    {
        IAsyncEnumerable<Batch> CreateBatches(Guid processId, int numberOfBatches);
        Task<Batch> GetStatus(Guid batchId);
    }
}
