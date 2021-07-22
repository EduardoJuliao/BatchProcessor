using System.Collections.Generic;
using System.Text;

namespace BatchProcessor.ProcessorApi.Extensions
{
    public static class AsyncEnumerableExtensions
    {
        public static  async IAsyncEnumerable<byte[]> ToHttpResponseDataItem<T>(this IAsyncEnumerable<T> enumerable)
        {
            await foreach (var item in enumerable)
            {
                string dataItem = $"data: {System.Text.Json.JsonSerializer.Serialize(item)}\n\n";

                yield return Encoding.UTF8.GetBytes(dataItem);
            }
            
        }
    }
}
