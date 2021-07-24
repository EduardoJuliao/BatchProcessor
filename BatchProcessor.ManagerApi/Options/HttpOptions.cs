using System;
namespace BatchProcessor.ManagerApi.Options
{
    public class HttpOptions
    {
        public string BaseUrl { get; set; }
        public string NumberGeneratorEndpoint { get; set; }
        public string NumberMultiplierEndpoint { get; set; }
    }
}
