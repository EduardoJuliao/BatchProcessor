using BatchProcessor.ProcessorApi.Extensions;
using BatchProcessor.ProcessorApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BatchProcessor.ProcessorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService ?? throw new ArgumentNullException(nameof(batchService));
        }

        [HttpGet("{processId:guid}/{amountOfBatches:int:min(0):max(10)}")]
        public async Task GenerateBatch(Guid processId, int amountOfBatches)
        {
            Response.SetEventStreamHeader();

            await foreach (var dataItemBytes in _batchService.CreateBatches(processId, amountOfBatches).ToHttpResponseDataItem())
                await Response.WriteContentToBody(dataItemBytes);
        }
    }
}
