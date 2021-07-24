using System;
using System.Text;

namespace BatchProcessor.Common.Extensions
{
    public static class TypeExtensions
    {
        public static byte[] ToHttpResponseDataItemBytes<T>(this T source, string type = "data")
        {
            return Encoding.UTF8.GetBytes(source.ToHttpResponseDataItem(type));
        }

        public static string ToHttpResponseDataItem<T>(this T source, string type = "data")
        {
            return $"{type}: {System.Text.Json.JsonSerializer.Serialize(source)}\n\n";
        }
    }
}
