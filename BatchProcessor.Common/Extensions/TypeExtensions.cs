using System;
using System.Text;

namespace BatchProcessor.Common.Extensions
{
    public static class TypeExtensions
    {
        public static byte[] ToHttpResponseDataItem<T>(this T source, string type = "data")
        {
            string dataItem = $"{type}: {System.Text.Json.JsonSerializer.Serialize(source)}\n\n";

            return Encoding.UTF8.GetBytes(dataItem);
        }
    }
}
