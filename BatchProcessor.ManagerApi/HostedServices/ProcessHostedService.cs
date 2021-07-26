using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BatchProcessor.ManagerApi.HostedServices
{
    public class ProcessHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProcessHostedService> _logger;
        private Timer _timer;


        public ProcessHostedService(IServiceProvider serviceProvider, ILogger<ProcessHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background service started at {at}", DateTime.UtcNow);

            _timer = new Timer(
                Process,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void Process(object state)
        {
            var processQueueService = _serviceProvider.GetRequiredService<IProcessQueueService>();

            var processes = processQueueService.GetQueuedProcesses().ToList();

            if (!processes.Any())
                return;

            _logger.LogInformation("{processCount} found in queue.",processes.Count());
            _logger.LogInformation($"Starting process work");

            using IServiceScope scope = _serviceProvider.CreateScope();

            var processService = scope.ServiceProvider.GetRequiredService<IProcessService>();

            Parallel.ForEach(processes, processId =>
            {
                try
                {
                    _logger.LogInformation("starting background work for process id: {processId}", processId);
                    processService.StartProcess(processId).GetAwaiter().GetResult();

                    _logger.LogInformation("Finishing process with id: {processId}", processId);
                    processService.FinishProcess(processId).GetAwaiter().GetResult();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Something went wrong while processing process with id: {processId}", processId);
                }
                
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
