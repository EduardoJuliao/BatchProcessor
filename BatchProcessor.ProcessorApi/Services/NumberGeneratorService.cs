using BatchProcessor.ProcessorApi.Interfaces.Services;
using BatchProcessor.ProcessorApi.Options;
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

        public NumberGeneratorService(
            IWorkerService processorService,
            NumberGeneratorOptions options)
        {
            _processorService = processorService ?? throw new ArgumentNullException(nameof(processorService));
            _numGenOptions = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Generates a list of new numbers
        /// </summary>
        /// <returns>A list of newly generated number</returns>
        public async IAsyncEnumerable<int> Generate(int amount)
        {
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

            await _processorService.Process();

            return _random.Next(min, max);
        }
    }
}
