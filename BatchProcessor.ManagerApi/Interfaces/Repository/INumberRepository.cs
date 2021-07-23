using BatchProcessor.ManagerApi.Entities;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Interfaces.Repository
{
    public interface INumberRepository
    {
        Task<Number> FindNumber(Guid id);
        Task<Number> UpdateNumber(Number number);
    }
}
