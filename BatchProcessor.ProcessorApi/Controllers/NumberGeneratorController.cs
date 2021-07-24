using BatchProcessor.ProcessorApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BatchProcessor.Common.Extensions;

namespace BatchProcessor.ProcessorApi.Controllers
{
    [Route("api/generator")]
    [ApiController]
    public class NumberGeneratorController : ControllerBase
    {
        private readonly INumberGeneratorService _numberGeneratorService;

        public NumberGeneratorController(INumberGeneratorService numberGeneratorService)
        {
            _numberGeneratorService = numberGeneratorService ?? throw new ArgumentNullException(nameof(numberGeneratorService));
        }

        /// <summary>
        /// Generates a list of numbers
        /// </summary>
        /// <param name="amountOfNumbers">amount of numbers to be generated</param>
        /// <returns>A list of generated numbers</returns>
        [HttpGet("{amountOfNumbers:int:min(1):max(10)}")]
        public async Task GenerateNumbers(int amountOfNumbers)
        {
            Response.SetEventStreamHeader();

            await foreach (var dataItemBytes in _numberGeneratorService.Generate(amountOfNumbers).ToHttpResponseDataItem())
                await Response.WriteContentToBody(dataItemBytes);
        }
    }
}
