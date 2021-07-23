using BatchProcessor.ManagerApi.Entities;

namespace BatchProcessor.ManagerApi.Interfaces.Factories
{
    public interface IProcessFactory
    {
        IProcessFactory SetBatchSize(int batchSize);
        IProcessFactory SetNumberPerBatch(int numberAmount);
        Process Build();
    }
}
