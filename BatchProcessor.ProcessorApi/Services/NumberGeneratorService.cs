using BatchProcessor.ProcessorApi.Interfaces.Services;
using BatchProcessor.ProcessorApi.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Services
{
    public class NumberGeneratorService : INumberGeneratorService
    {
        private static readonly Random _random = new Random();

        private readonly IWorkerService _processorService;
        private readonly NumberGeneratorOptions _numGenOptions;
        private readonly ILogger<NumberGeneratorService> _logger;

        public NumberGeneratorService(
            IWorkerService processorService,
            NumberGeneratorOptions options,
            ILogger<NumberGeneratorService> logger)
        {
            _processorService = processorService ?? throw new ArgumentNullException(nameof(processorService));
            _numGenOptions = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Generates a list of new numbers
        /// </summary>
        /// <returns>A list of newly generated number</returns>
        public async IAsyncEnumerable<int> Generate(int amount)
        {
            _logger.LogInformation("Starting the generation of {amount} numbers.", amount);
            for (var i = 0; i < amount; i++)
                yield return await Generate();
        }

        /// <summary>
        /// Generates a new number
        /// </summary>
        /// <returns>Newly generated number</returns>
        public async Task<int> Generate()
        {
            var min = _numGenOptions.MinValue;
            var max = _numGenOptions.MaxValue + (_numGenOptions.Inclusive ? 1 : 0);

            _logger.LogInformation("Starting number processing.");
            await _processorService.Process();

            var result = _random.Next(min, max);

            _logger.LogInformation("Number {numberValue} created.", result);

            return result;
        }
    }
}
