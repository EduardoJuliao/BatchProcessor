using System;
using BatchProcessor.ManagerApi.Entities;

namespace BatchProcessor.ManagerApi.Events.Data
{
    public class ProcessFinishedEventData : EventArgs
    {
        public Process Process;
    }
}
