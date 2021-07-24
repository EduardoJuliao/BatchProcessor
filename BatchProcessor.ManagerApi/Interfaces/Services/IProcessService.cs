using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Models;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Interfaces.Services
{
    public interface IProcessService
    {
        Task<ProcessModel> CreateProcess(int batchSize, int numberPerBatch);
        Task<ProcessModel> GetProcessStatus(Guid processId);
    }
}
