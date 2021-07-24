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

        public ProcessService(
            IProcessRepository processRepository,
            IProcessFactory processFactory
            )
        {
            _processRepository = processRepository ?? throw new ArgumentNullException(nameof(processRepository));
            _processFactory = processFactory ?? throw new ArgumentNullException(nameof(processFactory));
        }

        public async Task<ProcessModel> CreateProcess(int batchSize, int numberPerBatch)
        {
            var newProcess = _processFactory
                .SetBatchSize(batchSize)
                .SetNumberPerBatch(numberPerBatch)
                .Build();

            await _processRepository.CreateProcess(newProcess);

            return newProcess.Map();
        }

        public async Task<ProcessModel> GetProcessStatus(Guid processId)
        {
            return (await _processRepository.GetProcess(processId)).Map();
        }
    }
}
