using System;
using BatchProcessor.ManagerApi.Entities;

namespace BatchProcessor.ManagerApi.Interfaces.Factories
{
    public interface INumberFactory
    {
        INumberFactory SetOriginalValue(int value);
        INumberFactory SetOrder(int order);
        Number Build();
        INumberFactory SetBatchId(Guid batchId);
    }
}
