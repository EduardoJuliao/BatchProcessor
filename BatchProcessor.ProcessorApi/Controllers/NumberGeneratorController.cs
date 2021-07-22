using BatchProcessor.ProcessorApi.Extensions;
using BatchProcessor.ProcessorApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
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
            _numberGeneratorService = numberGeneratorService;
        }

        [HttpGet("")]
        public async Task GenerateNumbers(Guid batchId, int amountOfNumbers)
        {
            Response.Headers.Add("Content-Type", "text/event-stream");

            await foreach (var dataItemBytes in _numberGeneratorService.Generate(batchId, amountOfNumbers).ToHttpResponseDataItem())
                await Response.WriteContentToBody(dataItemBytes);
        }
    }
}
