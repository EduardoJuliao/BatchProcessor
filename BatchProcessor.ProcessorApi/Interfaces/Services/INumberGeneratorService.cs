using System.Collections.Generic;

namespace BatchProcessor.ProcessorApi.Interfaces.Services
{
    public interface INumberGeneratorService
    {
        IAsyncEnumerable<int> Generate(int amount);
    }
}
