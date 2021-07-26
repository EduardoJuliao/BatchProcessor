using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Repository
{
    public class ProcessRepository : IProcessRepository
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<ProcessRepository> _logger;

        public ProcessRepository(IApplicationContext context, ILogger<ProcessRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CreateProcess(Process newProcess)
        {
            _context.Processes.Add(newProcess);

            await ((DbContext)_context).SaveChangesAsync();

            _logger.LogInformation("Process {processId} created.", newProcess.Id);
        }

        public async Task<Process> AddBatchToProcess(Guid processId, Batch newBatch)
        {
            var process = await _context.Processes.FindAsync(processId);
            process.Batches.Add(newBatch);

            await ((DbContext)_context).SaveChangesAsync();

            _logger.LogInformation("Batch {batchId} added to process {processId}.", processId, newBatch.Id);

            return process;
        }

        public async Task<Process> UpdateProcess(Process process)
        {
            _context.Processes.Attach(process);

            await ((DbContext)_context).SaveChangesAsync();

            _logger.LogInformation("Process {processId} updated.", process.Id);

            return process;
        }

        public async Task<Process> GetProcess(Guid processId)
        {
            return await _context.Processes
                .Include(x => x.Batches)
                .ThenInclude(x => x.Numbers)
                .SingleAsync(x => x.Id == processId);
        }

        public async Task<Process> GetLastOrRecent()
        {
            if (await _context.Processes.CountAsync(x => x.IsFinished) == 1)
                return await _context.Processes
                    .Include(x => x.Batches)
                    .ThenInclude(x => x.Numbers)
                    .SingleOrDefaultAsync(x => x.IsFinished);

            return await _context.Processes
                .Include(x => x.Batches)
                .ThenInclude(x => x.Numbers)
                .Where(x => x.IsFinished)
                .OrderByDescending(x => x.FinishedAt)
                .FirstOrDefaultAsync();
        }
    }
}
