using System;
using BatchProcessor.ManagerApi.Entities;

namespace BatchProcessor.ManagerApi.Events.Data
{
    public class NumberMultipliedEventData : EventArgs
    {
        public Number Number { get; set; }
    }
}
