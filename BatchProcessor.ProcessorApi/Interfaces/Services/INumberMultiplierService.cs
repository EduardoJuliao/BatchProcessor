using BatchProcessor.ProcessorApi.Models;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface INumberMultiplierService
    {
        Task<int> Multiply(int value);
    }
}
