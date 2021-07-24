using System;
using BatchProcessor.ManagerApi.Entities;

namespace BatchProcessor.ManagerApi.Events.Data
{
    public class ProcessCreatedEventData : EventArgs
    {
        public Process Process;
    }
}
