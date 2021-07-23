using BatchProcessor.ManagerApi.Entities;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Interfaces.Services
{
    public interface IProcessService
    {
        Task<Process> CreateProcess(int batchSize, int numberPerBatch);
    }
}
