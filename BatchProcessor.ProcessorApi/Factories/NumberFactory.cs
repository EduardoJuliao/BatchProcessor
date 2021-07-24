using BatchProcessor.ProcessorApi.Interfaces.Factories;
using BatchProcessor.ProcessorApi.Models;

namespace BatchProcessor.ProcessorApi.Factories
{
    public class NumberFactory : INumberFactory
    {
        private int Multiplier { get; set; }
        private int OriginalValue { get; set; }

        public MultipliedNumberModel Build()
        {
            return new MultipliedNumberModel
            {
                Multiplier = Multiplier,
                OriginalValue = OriginalValue,
                MultipliedValue = OriginalValue * Multiplier

            };
        }

        public INumberFactory SetMultiplier(int multiplier)
        {
            Multiplier = multiplier;
            return this;
        }

        public INumberFactory SetOriginalValue(int value)
        {
            OriginalValue = value;
            return this;
        }
    }
}
