using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Entities;
using BatchProcessor.ManagerApi.Events.Data;
using BatchProcessor.ManagerApi.Interfaces.Managers;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Models;
using BatchProcessor.ManagerApi.Options;
using Microsoft.Extensions.Logging;

namespace BatchProcessor.ManagerApi.Managers
{
    public class MultiplyManager : IMultiplyManager
    {
        private readonly HttpClient _httpClient;
        private readonly HttpOptions _options;
        private readonly INumberRepository _numberRepository;
        private readonly ILogger<MultiplyManager> _logger;

        public MultiplyManager(HttpOptions options, INumberRepository numberRepository, ILogger<MultiplyManager> logger)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(options.BaseUrl)
            };
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _numberRepository = numberRepository ?? throw new ArgumentNullException(nameof(numberRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public event EventHandler<NumberMultipliedEventData> OnNumberMultiplied;

        public async Task<MultipliedNumberModel> Multiply(int value)
        {
            try
            {
                _logger.LogInformation("Starting multiply process for number {numberValue}.", value);
                var response = await _httpClient.GetAsync(_options.NumberMultiplierEndpoint + "/" + value);
                var json = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("Multiply process for number {numberValue} finished.", value);
                return JsonSerializer.Deserialize<MultipliedNumberModel>(json);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Couldn't multiply value.");
                throw ex;
            }
        }

        public async Task Multiply(Number number)
        {
            try
            {
                _logger.LogInformation("Starting multiply process for number {numberId}.", number.Id);
                var response = await _httpClient.GetAsync(_options.NumberMultiplierEndpoint + "/" + number.Value);
                var json = await response.Content.ReadAsStringAsync();

                var multiplied = JsonSerializer.Deserialize<MultipliedNumberModel>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                number.MultipliedValue = multiplied.MultipliedValue;
                number.Multiplier = multiplied.Multiplier;

                _numberRepository.UpdateNumber(number);

                _logger.LogInformation("Multiply process for number {numberId} finished.", number.Id);

                OnNumberMultiplied?.Invoke(this, new NumberMultipliedEventData { Number = number });
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Couldn't multiply value for number {numberId}.", number.Id);
                throw ex;
            }
            
        }
    }
}
