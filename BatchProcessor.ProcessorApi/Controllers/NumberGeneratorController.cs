using BatchProcessor.ProcessorApi.Extensions;
using BatchProcessor.ProcessorApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberGeneratorController : ControllerBase
    {
        private readonly INumberGeneratorService _numberGeneratorService;

        public NumberGeneratorController(INumberGeneratorService numberGeneratorService)
        {
            _numberGeneratorService = numberGeneratorService ?? throw new ArgumentNullException(nameof(numberGeneratorService));
        }

        [HttpGet("{batchId:guid}/{amountOfNumbers:int:min(0):max(100)}")]
        public async Task GenerateNumbers(Guid batchId, int amountOfNumbers)
        {
            Response.SetEventStreamHeader();

            await foreach (var dataItemBytes in _numberGeneratorService.Generate(batchId, amountOfNumbers).ToHttpResponseDataItem())
                await Response.WriteContentToBody(dataItemBytes);
        }
    }
}
