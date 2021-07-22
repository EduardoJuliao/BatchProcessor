using BatchProcessor.ProcessorApi.Entities;
using System;

namespace BatchProcessor.ProcessorApi.Interfaces.Factories
{
    public interface INumberFactory
    {
        INumberFactory SetOrder(int order);
        INumberFactory SetValue(int value);
        INumberFactory SetBatchId(Guid batchId);
        Number Build();
        
    }
}
