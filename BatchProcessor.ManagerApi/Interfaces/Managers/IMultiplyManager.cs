using System;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Models;

namespace BatchProcessor.ManagerApi.Interfaces.Managers
{
    public interface IMultiplyManager
    {
        Task Multiply(Guid numberId);
    }
}
