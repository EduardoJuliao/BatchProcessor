using System;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Interfaces.Managers
{
    public interface INumberManager
    {
        Task Generate(Guid batchId);
    }
}
