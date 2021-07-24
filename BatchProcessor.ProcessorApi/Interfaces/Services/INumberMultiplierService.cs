using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface INumberMultiplierService
    {
        /// <summary>
        /// Multiply a value
        /// </summary>
        /// <param name="value">Value to be multiplied</param>
        /// <returns>Multiplied number</returns>
        Task<int> Multiply(int value);
    }
}
