using BatchProcessor.ManagerApi.Entities;
using System;

namespace BatchProcessor.ManagerApi.Interfaces.Factories
{
    public interface IBatchFactory
    {
        IBatchFactory SetOrder(int order);
        IBatchFactory SetProcessId(Guid processId);
        Batch Build();
    }
}
