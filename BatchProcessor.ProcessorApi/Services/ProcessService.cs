using BatchProcessor.ProcessorApi.Entities;
using BatchProcessor.ProcessorApi.Interfaces.Factories;
using BatchProcessor.ProcessorApi.Interfaces.Repository;
using BatchProcessor.ProcessorApi.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IProcessRepository _processRepository;
        private readonly IProcessFactory _processFactory;

        public ProcessService(
            IProcessRepository processRepository, 
            IProcessFactory processFactory)
        {
            _processRepository = processRepository ?? throw new ArgumentNullException(nameof(processRepository));
            _processFactory = processFactory ?? throw new ArgumentNullException(nameof(processFactory));
        }

        public async Task<Process> CreateProcess(int batchSize, int numberPerBatch)
        {
            var newProcess = _processFactory
                .SetBatchSize(batchSize)
                .SetNumberPerBatch(numberPerBatch)
                .Build();

            await _processRepository.CreateProcess(newProcess);

            return newProcess;
        }
    }
}
