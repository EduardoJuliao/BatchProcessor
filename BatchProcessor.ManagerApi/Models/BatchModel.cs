using System;
using System.Collections.Generic;
using System.Linq;

namespace BatchProcessor.ManagerApi.Models
{
    public class BatchModel
    {
        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public int Order { get; set; }
        public int Size { get; set; }

        public int Sum =>
            Numbers == null
                ? 0
                : Numbers.Where(x => x.MultipliedValue.HasValue).Sum(x => x.MultipliedValue.Value);

        public int ProcessedNumbers =>
            Numbers == null
                ? 0
                : Numbers.Count(x => x.MultipliedValue.HasValue);

        public int RemainingNumbers
        {
            get {
                if (Numbers == null)
                    return Size;
                else
                    return Size - Numbers.Count(x => x.MultipliedValue.HasValue);
            }
        }

        public virtual IList<NumberModel> Numbers { get; set; }
    }
}
