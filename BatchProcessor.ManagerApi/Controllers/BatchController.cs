using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Interfaces.Services;
using BatchProcessor.ManagerApi.Mappers;
using BatchProcessor.ManagerApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BatchProcessor.ManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class BatchController : Controller
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        // GET: api/values
        [HttpGet("status/{batchId:guid}")]
        public async Task<BatchModel> Status(Guid batchId)
        {
            return (await _batchService.GetStatus(batchId)).Map();
        }
    }
}
