using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Repository
{
    public class NumberRepository : INumberRepository
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<NumberRepository> _logger;

        public NumberRepository(IApplicationContext context, ILogger<NumberRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CreateNumber(Number newNumber)
        {
            _context.Numbers.Add(newNumber);
            await ((DbContext)_context).SaveChangesAsync();

            _logger.LogInformation("New number created with id {numberId}", newNumber.Id);
        }

        public async Task<Number> FindNumber(Guid id)
        {
            return await _context.Numbers.FindAsync(id);
        }

        public void UpdateNumber(Number number)
        {
            lock(_context.Lock)
            {
                _context.Numbers.Attach(number);
                ((DbContext)_context).SaveChanges();

                _logger.LogInformation("Number {numberId} updated.", number.Id);
            }
        }

        public async Task UpdateNumberAsync(Number number)
        {
            _context.Numbers.Attach(number);
            await((DbContext)_context).SaveChangesAsync();

            _logger.LogInformation("Number {numberId} updated.", number.Id);
        }
    }
}
