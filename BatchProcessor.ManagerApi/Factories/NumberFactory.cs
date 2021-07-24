using System;
using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Factories;

namespace BatchProcessor.ManagerApi.Factories
{
    public class NumberFactory : INumberFactory
    {
        private Guid BatchId { get; set; }
        private int OriginalValue { get; set; }
        private int Order { get; set; }

        public Number Build()
        {
            return new Number
            {
                BatchId = BatchId,
                Order = Order,
                Value = OriginalValue
            };
        }

        public INumberFactory SetBatchId(Guid batchId)
        {
            BatchId = batchId;
            return this;
        }

        public INumberFactory SetOrder(int order)
        {
            Order = order;
            return this;
        }

        public INumberFactory SetOriginalValue(int value)
        {
            OriginalValue = value;
            return this;
        }
    }
}
