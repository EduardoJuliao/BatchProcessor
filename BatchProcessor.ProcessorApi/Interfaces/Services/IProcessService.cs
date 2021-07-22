using BatchProcessor.ProcessorApi.Entities;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface IProcessService
    {
        Task<Process> CreateProcess(int batchSize, int numberPerBatch);
    }
}
