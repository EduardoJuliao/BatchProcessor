using System;
namespace BatchProcessor.ManagerApi.Models
{
    public class NumberModel
    {
        public Guid Id { get; set; }
        public Guid BatchId { get; set; }
        public int Order { get; set; }
        public int Value { get; set; }
        public int? Multiplier { get; set; }
        public int? MultipliedValue { get; set; }
    }
}
