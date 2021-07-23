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

        [HttpGet("{amountOfNumbers:int:min(0):max(100)}")]
        public async Task GenerateNumbers(int amountOfNumbers)
        {
            Response.SetEventStreamHeader();

            await foreach (var dataItemBytes in _numberGeneratorService.Generate(amountOfNumbers).ToHttpResponseDataItem())
                await Response.WriteContentToBody(dataItemBytes);
        }
    }
}
