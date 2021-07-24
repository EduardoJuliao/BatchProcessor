using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface INumberGeneratorService
    {
        /// <summary>
        /// Generates a list of new numbers
        /// </summary>
        /// <returns>A list of newly generated number</returns>
        IAsyncEnumerable<int> Generate(int amount);

        /// <summary>
        /// Generates a new number
        /// </summary>
        /// <returns>Newly generated number</returns>
        Task<int> Generate();
    }
}
