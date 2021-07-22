using BatchProcessor.ProcessorApi.Entities;
using System;
using System.Collections.Generic;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface IBatchService
    {
        IAsyncEnumerable<Batch> CreateBatch(Guid processId, int numberOfBatches);
    }
}
