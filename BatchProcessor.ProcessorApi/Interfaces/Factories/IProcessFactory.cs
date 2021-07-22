using BatchProcessor.ProcessorApi.Entities;

namespace BatchProcessor.ProcessorApi.Interfaces.Factories
{
    public interface IProcessFactory
    {
        IProcessFactory SetBatchSize(int batchSize);
        IProcessFactory SetNumberPerBatch(int numberAmount);
        Process Build();
    }
}
