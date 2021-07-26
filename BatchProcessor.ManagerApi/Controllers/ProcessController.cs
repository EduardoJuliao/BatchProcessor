using System;
using System.Threading.Tasks;
using BatchProcessor.Common.Extensions;
using BatchProcessor.ManagerApi.Events.Data;
using BatchProcessor.ManagerApi.Interfaces.Services;
using BatchProcessor.ManagerApi.Mappers;
using BatchProcessor.ManagerApi.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("start/{batchSize:int:min(1):max(10)}/{numbersPerBatch:int:min(1):max(10)}")]
        public async Task StartProcess(int batchSize, int numbersPerBatch)
        {
            _processService.OnProcessCreated += OnProcessCreated;
            _processService.OnProcessFinished += OnProcessCompleted;
            _processService.OnNumberGenerated += OnNumberGenerated;
            _processService.OnNumberMultiplied += OnNumberMultiplied;

            Response.SetEventStreamHeader();

            var process = await Task.Run(()=>_processService.StartProcess(batchSize, numbersPerBatch)).ConfigureAwait(false);

            await _processService.FinishProcess(process.Id);
         }

        [HttpPost("create/{batchSize:int:min(1):max(10)}/{numbersPerBatch:int:min(1):max(10)}")]
        public async Task<ProcessModel> CreateProcess(int batchSize, int numbersPerBatch)
        {
            return (await _processService.CreateProcess(batchSize, numbersPerBatch)).Map();
        }

        [HttpPost("queue/{processId:Guid}")]
        public  OkObjectResult QueueProcess(Guid processId)
        {
            _processService.QueueProcess(processId);

            return new OkObjectResult(new { acknowledged = true });
        }

        [HttpGet("status/{processId:guid}")]
        public async Task<ProcessModel> GetProcessStatus(Guid processId)
        {
            return (await _processService.GetProcessStatus(processId)).Map();
        }

        [HttpGet("last")]
        public async Task<ProcessModel> GetLastProcess()
        {
            return (await _processService.GetLastProcess()).Map();
        }

        private async void OnProcessCreated(object sender, ProcessCreatedEventData data)
        {
            await Task.Run(() => Response.WriteContentToBody(data.Process.Map().ToHttpResponseDataItemBytes("process_started"))
                    .ConfigureAwait(false));
        }

        private async void OnProcessCompleted(object sender, ProcessFinishedEventData data)
        {
            await Task.Run(() => Response.WriteContentToBody(data.Process.Map().ToHttpResponseDataItemBytes("process_finished"))
                    .ConfigureAwait(false));
        }

        private async void OnNumberGenerated(object sender, NumberGeneratedEventData data)
        {
            await Task.Run(() => Response.WriteContentToBody(data.Number.Map().ToHttpResponseDataItemBytes("number_generated"))
                    .ConfigureAwait(false));
        }

        private async void OnNumberMultiplied(object sender, NumberMultipliedEventData data)
        {
            await Task.Run(() => Response.WriteContentToBody(data.Number.Map().ToHttpResponseDataItemBytes("number_multiplied"))
                    .ConfigureAwait(false));
        }
    }
}
