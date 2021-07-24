using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Repository
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

        public async Task<Process> GetProcess(Guid processId)
        {
            return await _context.Processes
                .Include(x => x.Batches)
                .ThenInclude(x => x.Numbers)
                .SingleAsync(x => x.Id == processId);
        }
    }
}
