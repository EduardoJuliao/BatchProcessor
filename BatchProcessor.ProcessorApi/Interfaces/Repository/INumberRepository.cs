using BatchProcessor.ProcessorApi.Entities;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Interfaces.Repository
{
    public interface INumberRepository
    {
        Task<Number> FindNumber(Guid id);
        Task<Number> UpdateNumber(Number number);
    }
}
