using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Factories;
using System;
using System.Collections.Generic;

namespace BatchProcessor.ManagerApi.Factories
{
    public class ProcessFactory : IProcessFactory
    {
        private int BatchSize { get; set; }
        private int NumbersPerbatch { get; set; }

        public Process Build()
        {
            return new Process
            {
                StartedAt = DateTime.UtcNow,
                BatchSize = this.BatchSize,
                NumbersPerBatch = this.NumbersPerbatch,
                IsFinished = false,
                Batches = new List<Batch>()
            };
        }

        public IProcessFactory SetBatchSize(int batchSize)
        {
            this.BatchSize = batchSize;
            return this;
        }

        public IProcessFactory SetNumberPerBatch(int numbersPerbatch)
        {
            this.NumbersPerbatch = numbersPerbatch;
            return this;
        }
    }
}
