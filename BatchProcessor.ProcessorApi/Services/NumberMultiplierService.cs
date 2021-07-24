using BatchProcessor.ProcessorApi.Interfaces.Services;
using BatchProcessor.ProcessorApi.Options;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Services
{
    public class NumberMultiplierService : INumberMultiplierService
    {
        private static readonly Random _random = new Random();

        private readonly IWorkerService _processorService;
        private readonly NumberMultiplierOptions _numberMultiplierOptions;

        public NumberMultiplierService(
            IWorkerService processorService,
            NumberMultiplierOptions numberMultiplierOptions)
        {
            _processorService = processorService ?? throw new ArgumentNullException(nameof(processorService));
            _numberMultiplierOptions = numberMultiplierOptions ?? throw new ArgumentNullException(nameof(numberMultiplierOptions));
        }

        /// <summary>
        /// Multiply a value
        /// </summary>
        /// <param name="value">Value to be multiplied</param>
        /// <returns>Multiplied number</returns>
        public async Task<int> Multiply(int value)
        {
            var min = _numberMultiplierOptions.MinValue;
            var max = _numberMultiplierOptions.MaxValue + (_numberMultiplierOptions.Inclusive ? 1 : 0); ;

            await _processorService.Process();

            var multiplier = _random.Next(min, max);

            return value * multiplier;
        }
    }
}
