using BatchProcessor.ProcessorApi.Interfaces.Factories;
using BatchProcessor.ProcessorApi.Interfaces.Services;
using BatchProcessor.ProcessorApi.Models;
using BatchProcessor.ProcessorApi.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Services
{
    public class NumberMultiplierService : INumberMultiplierService
    {
        private static readonly Random _random = new Random();

        private readonly IWorkerService _processorService;
        private readonly NumberMultiplierOptions _numberMultiplierOptions;
        private readonly INumberFactory _numberFactory;
        private readonly ILogger<NumberMultiplierService> _logger;

        public NumberMultiplierService(
            IWorkerService processorService,
            NumberMultiplierOptions numberMultiplierOptions,
            INumberFactory numberFactory,
            ILogger<NumberMultiplierService> logger)
        {
            _processorService = processorService ?? throw new ArgumentNullException(nameof(processorService));
            _numberMultiplierOptions = numberMultiplierOptions ?? throw new ArgumentNullException(nameof(numberMultiplierOptions));
            _numberFactory = numberFactory ?? throw new ArgumentNullException(nameof(numberFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Multiply a value
        /// </summary>
        /// <param name="value">Value to be multiplied</param>
        /// <returns>Multiplied number</returns>
        public async Task<MultipliedNumberModel> Multiply(int value)
        {
            _logger.LogInformation("Starting number multiply process.");
            var min = _numberMultiplierOptions.MinValue;
            var max = _numberMultiplierOptions.MaxValue + (_numberMultiplierOptions.Inclusive ? 1 : 0); ;

            await _processorService.Process();

            var multiplier = _random.Next(min, max);

            var multipliedNumber = _numberFactory
                .SetMultiplier(multiplier)
                .SetOriginalValue(value)
                .Build();

            _logger.LogInformation("Number {numberValue} multiplied to {numberMultiplied}.",
                multipliedNumber.OriginalValue,
                multipliedNumber.MultipliedValue);

            return multipliedNumber;
        }
    }
}
