using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BatchProcessor.Common.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteContentToBody(this HttpResponse httpResponse, byte[] utf8DataitemBytes)
        {
            await httpResponse.Body.WriteAsync(utf8DataitemBytes, 0, utf8DataitemBytes.Length);
            await httpResponse.Body.FlushAsync();
        }

        public static void SetEventStreamHeader(this HttpResponse httpResponse)
        {
            httpResponse.Headers.Add("Content-Type", "text/event-stream");
        }
    }
}
