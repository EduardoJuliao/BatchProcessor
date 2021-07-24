using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Events.Data;
using BatchProcessor.ManagerApi.Models;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Interfaces.Services
{
    public interface IProcessService
    {
        Task<Process> CreateProcess(int batchSize, int numbersPerBatch);
        Task<Process> StartProcess(int batchSize, int numberPerBatch);
        Task<Process> GetProcessStatus(Guid processId);
        Task FinishProcess(Guid id);

        event EventHandler<NumberGeneratedEventData> OnNumberGenerated;
        event EventHandler<NumberMultipliedEventData> OnNumberMultiplied;
        event EventHandler<ProcessCreatedEventData> OnProcessCreated;
        event EventHandler<ProcessCreatedEventData> OnProcessFinished;

        
    }
}
