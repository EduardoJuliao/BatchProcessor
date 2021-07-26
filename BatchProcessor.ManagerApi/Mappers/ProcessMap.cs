using System;
using System.Linq;
using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Models;

namespace BatchProcessor.ManagerApi.Mappers
{
    public static class ProcessMap
    {
        public static ProcessModel Map(this Process process)
        {
            if (process == null)
                return null;
            
            return new ProcessModel
            {
                BatchSize = process.BatchSize,
                Batches = process.Batches?.Select(x => x.Map()).ToList(),
                FinishedAt = process.FinishedAt,
                Id = process.Id,
                IsFinished = process.IsFinished,
                NumbersPerBatch = process.NumbersPerBatch,
                StartedAt = process.StartedAt
            };
        }
    }
}
