using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Repository
{
    public class NumberRepository : INumberRepository
    {
        private readonly IApplicationContext _context;

        public NumberRepository(IApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateNumber(Number newNumber)
        {
            _context.Numbers.Add(newNumber);
            await ((DbContext)_context).SaveChangesAsync();
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
            }
        }

        public async Task UpdateNumberAsync(Number number)
        {
            _context.Numbers.Attach(number);
            await((DbContext)_context).SaveChangesAsync();
        }
    }
}
