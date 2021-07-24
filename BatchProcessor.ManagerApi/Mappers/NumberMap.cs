using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Models;

namespace BatchProcessor.ManagerApi.Mappers
{
    public static class NumberMap
    {
        public static NumberModel Map(this Number number)
        {
            return new NumberModel
            {
                BatchId = number.BatchId,
                Order = number.Order,
                MultipliedValue = number.MultipliedValue,
                Multiplier = number.Multiplier,
                Value = number.Value,
                Id = number.Id
            };
        }
    }
}
