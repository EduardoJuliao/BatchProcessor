using System;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Events.Data;

namespace BatchProcessor.ManagerApi.Interfaces.Managers
{
    public interface IMultiplyManager
    {
        event EventHandler<NumberMultipliedEventData> OnNumberMultiplied;

        Task Multiply(Number number);
    }
}
