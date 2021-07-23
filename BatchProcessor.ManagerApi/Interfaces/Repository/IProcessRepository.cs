using BatchProcessor.ManagerApi.Entities;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Interfaces.Repository
{
    public interface IProcessRepository
    {
        Task CreateProcess(Process newProcess);
        Task<Process> AddBatchToProcess(Guid processId, Batch newBatch);
        Task<Process> UpdateProcess(Process process);
    }
}