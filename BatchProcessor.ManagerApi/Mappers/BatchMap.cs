using System;
using System.Linq;
using System.Runtime.CompilerServices;
using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Models;

namespace BatchProcessor.ManagerApi.Mappers
{
    public static class BatchMap
    {
        public static BatchModel Map(this Batch batch)
        {
            return new BatchModel
            {
                Id = batch.Id,
                Numbers = batch.Numbers?.Select(x => x.Map()).ToList(),
                Order = batch.Order,
                ProcessId = batch.ProcessId,
                Size = batch.Size
            };
        }
    }
}
