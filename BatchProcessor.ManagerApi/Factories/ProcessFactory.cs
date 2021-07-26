using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Factories
{
    public class ProcessFactory : IProcessFactory
    {
        private int BatchSize { get; set; }
        private int NumbersPerbatch { get; set; }
        private List<Batch> Batches { get; set; }

        public Process Build()
        {
            return new Process
            {
                StartedAt = DateTime.UtcNow,
                BatchSize = this.BatchSize,
                NumbersPerBatch = this.NumbersPerbatch,
                IsFinished = false,
                Batches = Batches ?? new List<Batch>()
            };
        }

        public IProcessFactory SetBatchSize(int batchSize)
        {
            this.BatchSize = batchSize;

            this.Batches = Enumerable.Range(0, batchSize).Select(x => new Batch
            {
                Order = x,
                Size = batchSize,
                Numbers = new List<Number>()
            }).ToList();

            return this;
        }

        public IProcessFactory SetNumberPerBatch(int numbersPerbatch)
        {
            this.NumbersPerbatch = numbersPerbatch;

            return this;
        }
    }
}
