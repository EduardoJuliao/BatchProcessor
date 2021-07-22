using System;
using System.Collections.Generic;

namespace BatchProcessor.ProcessorApi.Entities
{
    public class Process
    {
        public Guid Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public bool IsFinished { get; set; }
        public int BatchSize { get; set; }
        public int NumbersPerBatch { get; set; }
        public virtual IList<Batch> Batches { get; set; }
    }
}
