using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Events.Data;
using BatchProcessor.ManagerApi.Interfaces.Factories;
using BatchProcessor.ManagerApi.Interfaces.Managers;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IProcessRepository _processRepository;
        private readonly IProcessFactory _processFactory;
        private readonly INumberManager _numberManager;
        private readonly IProcessQueueService _processQueueService;
        private readonly ILogger<ProcessService> _logger;

        public event EventHandler<NumberGeneratedEventData> OnNumberGenerated;
        public event EventHandler<NumberMultipliedEventData> OnNumberMultiplied;
        public event EventHandler<ProcessCreatedEventData> OnProcessCreated;
        public event EventHandler<ProcessFinishedEventData> OnProcessFinished;

        public ProcessService(
            IProcessRepository processRepository,
            IProcessFactory processFactory,
            INumberManager numberManager,
            IProcessQueueService processQueueService,
            ILogger<ProcessService> logger)
        {
            _processRepository = processRepository ?? throw new ArgumentNullException(nameof(processRepository));
            _processFactory = processFactory ?? throw new ArgumentNullException(nameof(processFactory));
            _numberManager = numberManager ?? throw new ArgumentNullException(nameof(numberManager));
            _processQueueService = processQueueService ?? throw new ArgumentNullException(nameof(processQueueService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            _logger.LogInformation("Finishing process {processId}", id);
            var process = await _processRepository.GetProcess(id);
            process.IsFinished = true;
            process.FinishedAt = DateTime.UtcNow;

            await _processRepository.UpdateProcess(process);

            OnProcessFinished?.Invoke(this, new ProcessFinishedEventData { Process = process });
        }

        public async Task<Process> CreateProcess(int batchSize, int numbersPerBatch)
        {
            _logger.LogInformation("Creating a new process with {batchSize} number of batches, {numbersPerBatch} amount of numbers each.",
                batchSize,
                numbersPerBatch);

            var newProcess = _processFactory
                .SetAmountOfBatches(batchSize, numbersPerBatch)
                .Build();

            await _processRepository.CreateProcess(newProcess);

            _logger.LogInformation("New process with Id {processId} created.", newProcess.Id);

            OnProcessCreated?.Invoke(this, new ProcessCreatedEventData { Process = newProcess });

            return newProcess;
        }

        public void QueueProcess(Guid processId)
        {
            _processQueueService.AddProcessToQueue(processId);
            _logger.LogInformation("Process {processId} queued.", processId);
        }

        public async Task StartProcess(Guid processId)
        {
            _logger.LogInformation("Starting number generation for process {processId}", processId);

            var process = await _processRepository.GetProcess(processId);

            await _numberManager.Generate(process);
        }

        public async Task<Process> GetLastProcess()
        {
            return await _processRepository.GetLastOrRecent();
        }
    }
}
