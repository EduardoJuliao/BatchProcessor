using BatchProcessor.ProcessorApi.Entities;
using BatchProcessor.ProcessorApi.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Factories
{
    public class NumberFactory : INumberFactory
    {
        private Guid BatchId { get; set; }
        private int Order { get; set; }
        private int Value { get; set; }

        public Number Build()
        {
            return new Number
            {
                Order = Order,
                BatchId = BatchId,
                Value = Value
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

        public INumberFactory SetValue(int value)
        {
            Value = value;
            return this;
        }
    }
}
