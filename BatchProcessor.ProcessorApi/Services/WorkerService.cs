using BatchProcessor.ProcessorApi.Interfaces.Services;
using BatchProcessor.ProcessorApi.Options;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Services
{
    public class WorkerService : IWorkerService
    {
        private static readonly Random _random = new Random();
        private readonly ProcessOptions _options;

        public WorkerService(ProcessOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Process data
        /// </summary>
        /// <returns></returns>
        public async Task Process()
        {
            var min = _options.MinTimeInSeconds;
            var max = _options.MaxTimeInSeconds + (_options.Inclusive ? 1 : 0);

            var delay = _random.Next(min, max);
            await Task.Delay(TimeSpan.FromSeconds(delay));
        }
    }
}
