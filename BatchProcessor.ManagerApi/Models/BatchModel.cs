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

        public string Progress => ((
            (Numbers == null ? 0 : Numbers.Count(x => x.MultipliedValue.HasValue)) / Size) * 100)
            .ToString() + "%";

        public virtual IList<NumberModel> Numbers { get; set; }
    }
}
