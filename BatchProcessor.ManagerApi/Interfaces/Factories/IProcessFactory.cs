using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Entities;

namespace BatchProcessor.ManagerApi.Interfaces.Factories
{
    public interface IProcessFactory
    {
        IProcessFactory SetAmountOfBatches(int batchSize, int amountOfNumbers);
        Process Build();
    }
}
