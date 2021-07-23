using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Factories;
using System;
using System.Collections.Generic;

namespace BatchProcessor.ManagerApi.Factories
{
    public class BatchFactory : IBatchFactory
    {
        private int Order { get; set; }
        private Guid ProcessId { get; set; }

        public Batch Build()
        {
            return new Batch
            {
                Numbers = new List<Number>(),
                Order = Order,
                ProcessId = ProcessId
            };
        }

        public IBatchFactory SetOrder(int order)
        {
            Order = order;
            return this;
        }

        public IBatchFactory SetProcessId(Guid processId)
        {
            ProcessId = processId;
            return this;
        }
    }
}
