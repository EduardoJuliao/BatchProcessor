using System;
using System.Collections.Generic;

namespace BatchProcessor.ManagerApi.Entities
{
    public class Batch
    {
        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public int Order { get; set; }
        public int Size { get; set; }

        public virtual IList<Number> Numbers { get; set; }
        public virtual Process Process { get; set; }
    }
}
