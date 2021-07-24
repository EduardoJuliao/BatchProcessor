using System.Threading.Tasks;
using BatchProcessor.ProcessorApi.Interfaces.Services;

namespace BatchProcessor.ProcessorApi.Tests.Moqs
{
    public class WorkerServiceMoq : IWorkerService
    {
        public async Task Process()
        {
            await Task.Delay(5000);
        }
    }
}
