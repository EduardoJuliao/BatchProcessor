using System;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Events.Data;

namespace BatchProcessor.ManagerApi.Interfaces.Managers
{
    public interface INumberManager
    {
        Task<Batch> Generate(Batch batch);
        Task Generate(Process process);

        event EventHandler<NumberMultipliedEventData> OnNumberMultiplied;
        event EventHandler<NumberGeneratedEventData> OnNumberGenerated;
    }
}
