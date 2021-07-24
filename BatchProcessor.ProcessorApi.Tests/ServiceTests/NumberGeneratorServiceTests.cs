using System.Collections.Generic;
using System.Threading.Tasks;
using BatchProcessor.ProcessorApi.Interfaces.Services;
using BatchProcessor.ProcessorApi.Services;
using BatchProcessor.ProcessorApi.Tests.Moqs;
using NUnit.Framework;

namespace BatchProcessor.ProcessorApi.Tests.ServiceTests
{
    public class NumberGeneratorServiceTests
    {
        private INumberGeneratorService _service;

        [SetUp]
        public void Setup()
        {
            _service = new NumberGeneratorService(new WorkerServiceMoq(), new Options.NumberGeneratorOptions
            {
                Inclusive = true,
                MaxValue = 100,
                MinValue = 1
            });
        }

        [Test]
        public async Task Test1()
        {
        }
    }
}
