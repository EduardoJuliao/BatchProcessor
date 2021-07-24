using BatchProcessor.ProcessorApi.Interfaces.Services;
using BatchProcessor.ProcessorApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Controllers
{
    [Route("api/multiplier")]
    [ApiController]
    public class NumberMultiplierController : ControllerBase
    {
        private readonly INumberMultiplierService _numberMultiplierService;

        public NumberMultiplierController(INumberMultiplierService numberMultiplierService)
        {
            _numberMultiplierService = numberMultiplierService ?? throw new ArgumentNullException(nameof(numberMultiplierService));
        }

        [HttpGet("{value:int}")]
        public async Task<MultipliedNumberModel> MultiplyNumber(int value)
            => await _numberMultiplierService.Multiply(value);
    }
}
