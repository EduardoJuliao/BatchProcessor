using System.Threading.Tasks;
using BatchProcessor.ProcessorApi.Models;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface INumberMultiplierService
    {
        /// <summary>
        /// Multiply a value
        /// </summary>
        /// <param name="value">Value to be multiplied</param>
        /// <returns>Multiplied number</returns>
        Task<MultipliedNumberModel> Multiply(int value);
    }
}
