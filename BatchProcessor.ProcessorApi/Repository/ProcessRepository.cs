using BatchProcessor.ProcessorApi.Entities;
using BatchProcessor.ProcessorApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Repository
{
    public class ProcessRepository : IProcessRepository
    {
        private readonly IApplicationContext _context;

        public ProcessRepository(IApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateProcess(Process newProcess)
        {
            _context.Processes.Add(newProcess);

            await ((DbContext)_context).SaveChangesAsync();
        }

        public async Task<Process> AddBatchToProcess(Guid processId, Batch newBatch)
        {
            var process = await _context.Processes.FindAsync(processId);
            process.Batches.Add(newBatch);

            await ((DbContext)_context).SaveChangesAsync();

            return process;
        }

        public async Task<Process> UpdateProcess(Process process)
        {
            _context.Processes.Attach(process);

            await ((DbContext)_context).SaveChangesAsync();

            return process;
        }
    }
}
