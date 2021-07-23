using BatchProcessor.ProcessorApi.Interfaces.Services;
using BatchProcessor.ProcessorApi.Options;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public async IAsyncEnumerable<int> Generate(int amount)
        {
            var min = _numGenOptions.MinValue;
            var max = _numGenOptions.MaxValue;

            foreach (var number in Enumerable.Range(0, amount).Select(x => _random.Next(min, max)))
            {
                await _processorService.Process();

                yield return number;
            }
        }
    }
}
