using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Events.Data;

namespace BatchProcessor.ManagerApi.Interfaces.Managers
{
    public interface INumberManager
    {
        Task Generate(Batch batch);
        void Generate(Process process);

        event EventHandler<NumberMultipliedEventData> OnNumberMultiplied;
        event EventHandler<NumberGeneratedEventData> OnNumberGenerated;
    }
}
