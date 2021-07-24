using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface IWorkerService
    {
        /// <summary>
        /// Process data
        /// </summary>
        /// <returns></returns>
        Task Process();
    }
}
