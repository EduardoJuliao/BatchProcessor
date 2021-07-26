using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Interfaces.Managers;
using BatchProcessor.ManagerApi.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BatchProcessor.ManagerApi.HostedServices
{
    public class ProcessHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;


        public ProcessHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
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

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                
                var processService = scope.ServiceProvider.GetRequiredService<IProcessService>();

                Parallel.ForEach(processes, processId =>
                {
                    processService.StartProcess(processId).GetAwaiter().GetResult();

                    processService.FinishProcess(processId).GetAwaiter().GetResult();
                });
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
