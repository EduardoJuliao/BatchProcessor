using System;
using System.Collections.Generic;
using System.Linq;
using BatchProcessor.ManagerApi.Interfaces.Services;

namespace BatchProcessor.ManagerApi.Services
{
    public class ProcessQueueService : IProcessQueueService
    {
        private readonly Queue<Guid> _processes = new Queue<Guid>();

        private readonly object _lock = new object();

        public void AddProcessToQueue(Guid processId)
        {
            lock(_lock)
            {
                _processes.Enqueue(processId);
            }
        }

        public IEnumerable<Guid> GetQueuedProcesses()
        {
            lock(_lock)
            {
                while (_processes.Any())
                    yield return _processes.Dequeue();
            }            
        }
        
    }
}
