using BatchProcessor.ProcessorApi.Entities;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface INumberMultiplierService
    {
        Task<Number> Multiply(Guid numberId);
    }
}
