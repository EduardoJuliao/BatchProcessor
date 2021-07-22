using BatchProcessor.ProcessorApi.Entities;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Interfaces.Repository
{
    public interface IProcessRepository
    {
        Task CreateProcess(Process newProcess);
        Task<Process> AddBatchToProcess(Guid processId, Batch newBatch);
        Task<Process> UpdateProcess(Process process);
    }
}