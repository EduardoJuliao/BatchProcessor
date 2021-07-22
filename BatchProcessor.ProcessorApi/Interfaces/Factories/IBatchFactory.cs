using BatchProcessor.ProcessorApi.Entities;
using System;

namespace BatchProcessor.ProcessorApi.Interfaces.Factories
{
    public interface IBatchFactory
    {
        IBatchFactory SetOrder(int order);
        IBatchFactory SetProcessId(Guid processId);
        Batch Build();
    }
}
