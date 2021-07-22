using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface IWorkerService
    {
        Task Process();
    }
}
