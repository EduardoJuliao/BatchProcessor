﻿using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BatchProcessor.ManagerApi.Interfaces.Managers;
using BatchProcessor.ManagerApi.Interfaces.Repository;
using BatchProcessor.ManagerApi.Models;
using BatchProcessor.ManagerApi.Options;

namespace BatchProcessor.ManagerApi.Managers
{
    public class MultiplyManager : IMultiplyManager
    {
        private readonly HttpClient _httpClient;
        private readonly HttpOptions _options;
        private readonly INumberRepository _numberRepository;

        public MultiplyManager(HttpOptions options, INumberRepository numberRepository)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(options.BaseUrl)
            };
            _options = options;
            _numberRepository = numberRepository;
        }

        public async Task<MultipliedNumberModel> Multiply(int value)
        {
            var response = await _httpClient.GetAsync(_options.NumberMultiplierEndpoint + "/" + value);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MultipliedNumberModel>(json);
        }

        public async Task Multiply(Guid numberId)
        {
            var number = await _numberRepository.FindNumber(numberId);
            var response = await _httpClient.GetAsync(_options.NumberMultiplierEndpoint + "/" + number.Value);
            var json = await response.Content.ReadAsStringAsync();
            var multiplied = JsonSerializer.Deserialize<MultipliedNumberModel>(json);

            number.MultipliedValue = multiplied.MultipliedValue;
            number.Multiplier = multiplied.Multiplier;

            await _numberRepository.UpdateNumber(number);
        }
    }
}