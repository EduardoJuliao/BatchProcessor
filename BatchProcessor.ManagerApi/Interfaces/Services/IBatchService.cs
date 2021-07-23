using BatchProcessor.ManagerApi.Entities;
using System;
using System.Collections.Generic;

namespace BatchProcessor.ManagerApi.Interfaces.Services
{
    public interface IBatchService
    {
        IAsyncEnumerable<Batch> CreateBatches(Guid processId, int numberOfBatches);
    }
}
