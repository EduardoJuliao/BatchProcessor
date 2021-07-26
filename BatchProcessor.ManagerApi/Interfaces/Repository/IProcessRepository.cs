using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Models;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Interfaces.Repository
{
    public interface IProcessRepository
    {
        Task CreateProcess(Process newProcess);
        Task<Process> AddBatchToProcess(Guid processId, Batch newBatch);
        Task<Process> UpdateProcess(Process process);
        Task<Process> GetProcess(Guid processId);
        Task<Process> GetLastOrRecent();
    }
}