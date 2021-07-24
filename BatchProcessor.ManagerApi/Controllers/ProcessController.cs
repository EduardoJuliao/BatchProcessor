﻿using System;
using System.Threading.Tasks;
using BatchProcessor.Common.Extensions;
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
            _processService.OnProcessCreated += async (sender, data) =>
            {
                await Task.Run(() => Response.WriteContentToBody(data.Process.Map().ToHttpResponseDataItemBytes("process_started"))
                    .ConfigureAwait(false));
            };

            _processService.OnProcessFinished += async (sender, data) =>
            {
                await Task.Run(() => Response.WriteContentToBody(data.Process.Map().ToHttpResponseDataItemBytes("process_finished"))
                    .ConfigureAwait(false));
            };

            _processService.OnNumberGenerated += async (sender, data) => {

                await Task.Run(() => Response.WriteContentToBody(data.Number.Map().ToHttpResponseDataItemBytes("number_generated"))
                    .ConfigureAwait(false));
            };

            _processService.OnNumberMultiplied += async (sender, data) =>
            {
                await Task.Run(() => Response.WriteContentToBody(data.Number.Map().ToHttpResponseDataItemBytes("number_multiplied"))
                    .ConfigureAwait(false));
            };

            Response.SetEventStreamHeader();

            var process = await Task.Run(()=>_processService.StartProcess(batchSize, numbersPerBatch)).ConfigureAwait(false);

            await _processService.FinishProcess(process.Id);
         }

        [HttpGet("status/{processId:guid}")]
        public async Task<ProcessModel> GetProcessStatus(Guid processId)
        {
            return (await _processService.GetProcessStatus(processId)).Map();
        }
    }
}
