using BatchProcessor.ProcessorApi.Models;

namespace BatchProcessor.ProcessorApi.Interfaces.Factories
{
    public interface INumberFactory
    {
        INumberFactory SetOriginalValue(int value);
        INumberFactory SetMultiplier(int multiplier);
        MultipliedNumberModel Build(); 
    }
}
