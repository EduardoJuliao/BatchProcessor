using System;

namespace BatchProcessor.ProcessorApi.Entities
{
    public class Number
    {
        public Guid Id { get; set; }
        public Guid BatchId { get; set; }
        public int Order { get; set; }
        public int Value { get; set; }
        public int Multiplier { get; set; }
        public int MultipliedValue { get; set; }
        public virtual Batch Batch { get; set; }
    }
}
