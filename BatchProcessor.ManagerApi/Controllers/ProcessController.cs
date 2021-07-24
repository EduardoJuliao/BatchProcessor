using System;
using System.Threading.Tasks;
using BatchProcessor.Common.Extensions;
using BatchProcessor.ManagerApi.Interfaces.Services;
using BatchProcessor.ManagerApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BatchProcessor.ManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class ProcessController : Controller
    {
        private readonly IProcessService _processService;

        public ProcessController(IProcessService processService)
        {
            _processService = processService ?? throw new System.ArgumentNullException(nameof(processService));
        }

        [HttpPost("start/{batchSize:int:min(1):max(10)}/{numbersPerBatch:int:min(1):max(10)}")]
        public async Task StartProcess(int batchSize, int numbersPerBatch)
        {
            Response.SetEventStreamHeader();

            var process = await _processService.CreateProcess(batchSize, numbersPerBatch);

            await Response.WriteContentToBody(process.ToHttpResponseDataItem("process"));
        }

        [HttpGet("status/{processId:guid}")]
        public async Task<ProcessModel> GetProcessStatus(Guid processId)
        {
            return await _processService.GetProcessStatus(processId);
        }
    }
}
