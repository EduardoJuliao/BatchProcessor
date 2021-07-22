using BatchProcessor.ProcessorApi.Entities;
using BatchProcessor.ProcessorApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Repository
{
    public class NumberRepository : INumberRepository
    {
        private readonly IApplicationContext _context;

        public NumberRepository(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Number> FindNumber(Guid id)
        {
            return await _context.Numbers.FindAsync(id);
        }

        public async Task<Number> UpdateNumber(Number number)
        {
            _context.Numbers.Attach(number);
            await ((DbContext)_context).SaveChangesAsync();
            return number;
        }
    }
}
