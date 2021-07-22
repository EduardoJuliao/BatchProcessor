using BatchProcessor.ProcessorApi.Entities;
using System;
using System.Collections.Generic;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface INumberGeneratorService
    {
        IAsyncEnumerable<Number> Generate(Guid batchId, int amount);
    }
}
