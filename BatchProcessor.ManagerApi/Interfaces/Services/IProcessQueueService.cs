using System;
using System.Collections.Generic;

namespace BatchProcessor.ManagerApi.Interfaces.Services
{
    public interface IProcessQueueService
    {
        void AddProcessToQueue(Guid ProcessId);
        IEnumerable<Guid> GetQueuedProcesses();
    }
}
