using BatchProcessor.ProcessorApi.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Services
{
    public class WorkerService : IWorkerService
    {
        private static readonly Random _random = new Random();

        public async Task Process()
        {
            var delay = _random.Next(5, 10);
            await Task.Delay(TimeSpan.FromSeconds(delay));
        }
    }
}
