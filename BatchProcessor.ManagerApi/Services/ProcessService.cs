using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Events.Data;
using BatchProcessor.ManagerApi.Interfaces.Factories;
using BatchProcessor.ManagerApi.Interfaces.Managers;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Interfaces.Services;
using BatchProcessor.ManagerApi.Mappers;
using BatchProcessor.ManagerApi.Models;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IProcessRepository _processRepository;
        private readonly IProcessFactory _processFactory;
        private readonly INumberManager _numberManager;

        public event EventHandler<NumberGeneratedEventData> OnNumberGenerated;
        public event EventHandler<NumberMultipliedEventData> OnNumberMultiplied;
        public event EventHandler<ProcessCreatedEventData> OnProcessCreated;
        public event EventHandler<ProcessFinishedEventData> OnProcessFinished;

        public ProcessService(
            IProcessRepository processRepository,
            IProcessFactory processFactory,
            INumberManager numberManager)
        {
            _processRepository = processRepository ?? throw new ArgumentNullException(nameof(processRepository));
            _processFactory = processFactory ?? throw new ArgumentNullException(nameof(processFactory));
            _numberManager = numberManager ?? throw new ArgumentNullException(nameof(numberManager));
        }

        public async Task<Process> StartProcess(int batchSize, int numberPerBatch)
        {
            var newProcess = await CreateProcess(batchSize, numberPerBatch);

            _numberManager.OnNumberGenerated += OnNumberGenerated;
            _numberManager.OnNumberMultiplied += OnNumberMultiplied;

            await _numberManager.Generate(newProcess).ConfigureAwait(false);

            return newProcess;
        }

        public async Task<Process> GetProcessStatus(Guid processId)
        {
            return (await _processRepository.GetProcess(processId));
        }

        public async Task FinishProcess(Guid id)
        {
            var process = await _processRepository.GetProcess(id);
            process.IsFinished = true;
            process.FinishedAt = DateTime.UtcNow;

            await _processRepository.UpdateProcess(process);

            OnProcessFinished?.Invoke(this, new ProcessFinishedEventData { Process = process });
        }

        public async Task<Process> CreateProcess(int batchSize, int numbersPerBatch)
        {
            var newProcess = _processFactory
                .SetBatchSize(batchSize)
                .SetNumberPerBatch(numbersPerBatch)
                .Build();

            await _processRepository.CreateProcess(newProcess);

            OnProcessCreated?.Invoke(this, new ProcessCreatedEventData { Process = newProcess });

            return newProcess;
        }
    }
}
