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

        public IProcessFactory SetAmountOfBatches(int batchSize, int numbersPerBatch)
        {
            this.BatchSize = batchSize;

            this.Batches = Enumerable.Range(0, batchSize).Select(x => new Batch
            {
                Order = x,
                Size = numbersPerBatch,
                Numbers = new List<Number>()
            }).ToList();

            return this;
        }
    }
}
