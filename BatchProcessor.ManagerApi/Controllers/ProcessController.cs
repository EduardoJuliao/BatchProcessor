using System;
using System.Threading.Tasks;
using BatchProcessor.Common.Extensions;
using BatchProcessor.ManagerApi.Events.Data;
using BatchProcessor.ManagerApi.Interfaces.Services;
using BatchProcessor.ManagerApi.Mappers;
using BatchProcessor.ManagerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BatchProcessor.ManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class ProcessController : Controller
    {
        private readonly IProcessService _processService;
        private readonly ILogger<ProcessController> _logger;

        public ProcessController(IProcessService processService, ILogger<ProcessController> logger)
        {
            _processService = processService ?? throw new System.ArgumentNullException(nameof(processService));
            _logger = logger;
        }

        [HttpGet("start/{batchSize:int:min(1):max(10)}/{numbersPerBatch:int:min(1):max(10)}")]
        public async Task StartProcess(int batchSize, int numbersPerBatch)
        {
            _processService.OnProcessCreated += OnProcessCreated;
            _processService.OnProcessFinished += OnProcessCompleted;
            _processService.OnNumberGenerated += OnNumberGenerated;
            _processService.OnNumberMultiplied += OnNumberMultiplied;

            Response.SetEventStreamHeader();

            _logger.LogInformation("Starting the process for {batchSize} batches with {amountOfNumbers} per batch.", batchSize, numbersPerBatch);
            var process = await Task.Run(()=>_processService.StartProcess(batchSize, numbersPerBatch)).ConfigureAwait(false);

            _logger.LogInformation("Finishing work for process with id: {processId}", process.Id);
            await _processService.FinishProcess(process.Id);
         }

        [HttpPost("create/{batchSize:int:min(1):max(10)}/{numbersPerBatch:int:min(1):max(10)}")]
        public async Task<ProcessModel> CreateProcess(int batchSize, int numbersPerBatch)
        {
            _logger.LogInformation("Creating the process for {batchSize} batches with {amountOfNumbers} per batch.", batchSize, numbersPerBatch);
            return (await _processService.CreateProcess(batchSize, numbersPerBatch)).Map();
        }

        [HttpPost("queue/{processId:Guid}")]
        public  OkObjectResult QueueProcess(Guid processId)
        {
            _logger.LogInformation("Queuing the process id {processId}", processId);
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
            _logger.LogInformation("Sending http message for process created, id: {processId}", data.Process.Id);
            await Task.Run(() => Response.WriteContentToBody(data.Process.Map().ToHttpResponseDataItemBytes("process_started"))
                    .ConfigureAwait(false));
        }

        private async void OnProcessCompleted(object sender, ProcessFinishedEventData data)
        {
            _logger.LogInformation("Sending http message for process completed, id: {processId}", data.Process.Id);
            await Task.Run(() => Response.WriteContentToBody(data.Process.Map().ToHttpResponseDataItemBytes("process_finished"))
                    .ConfigureAwait(false));
        }

        private async void OnNumberGenerated(object sender, NumberGeneratedEventData data)
        {
            _logger.LogInformation("Sending http message for number created, id: {processId}, number: {numberValue}",
                data.Number.Id,
                data.Number.Value);
            await Task.Run(() => Response.WriteContentToBody(data.Number.Map().ToHttpResponseDataItemBytes("number_generated"))
                    .ConfigureAwait(false));
        }

        private async void OnNumberMultiplied(object sender, NumberMultipliedEventData data)
        {
            _logger.LogInformation("Sending http message for number multiplied, id: {processId}, number: {numberValue}",
                data.Number.Id,
                data.Number.MultipliedValue);
            await Task.Run(() => Response.WriteContentToBody(data.Number.Map().ToHttpResponseDataItemBytes("number_multiplied"))
                    .ConfigureAwait(false));
        }
    }
}
