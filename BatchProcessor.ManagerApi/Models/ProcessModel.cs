using System;
using System.Collections.Generic;

namespace BatchProcessor.ManagerApi.Models
{
    public class ProcessModel
    {
        public Guid Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public bool IsFinished { get; set; }
        public int BatchSize { get; set; }
        public int NumbersPerBatch { get; set; }
        public virtual IList<BatchModel> Batches { get; set; }
    }
}
