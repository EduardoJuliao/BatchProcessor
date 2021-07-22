using BatchProcessor.ProcessorApi.Entities;
using BatchProcessor.ProcessorApi.Interfaces.Repository;
using BatchProcessor.ProcessorApi.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Services
{
    public class NumberMultiplierService : INumberMultiplierService
    {
        private static readonly Random _random = new Random();

        private readonly IWorkerService _processorService;
        private readonly INumberRepository _numberRepository;

        public NumberMultiplierService(
            IWorkerService processorService, 
            INumberRepository numberRepository)
        {
            _processorService = processorService ?? throw new ArgumentNullException(nameof(processorService));
            _numberRepository = numberRepository ?? throw new ArgumentNullException(nameof(numberRepository));
        }

        public async Task<Number> Multiply(Guid numberId)
        {
            await _processorService.Process();

            var number = await _numberRepository.FindNumber(numberId);

            var multiplier = _random.Next(2, 4);

            number.Multiplier = multiplier;
            number.MultipliedValue = number.Value * multiplier;

            return await _numberRepository.UpdateNumber(number);

        }
    }
}
