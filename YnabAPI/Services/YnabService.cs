using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using YnabAPI.ExternalModels;

namespace YnabAPI.Services
{
    public interface IYnabService
    {
        Task<YnabResponse> GetTransactions(string budgetId, string? startDate);
    }

    public class YnabService : IYnabService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public YnabService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;

            httpClient.BaseAddress = new Uri(configuration["YnabService:BaseUrl"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.configuration["YnabService:AccessToken"]);
        }

        public async Task<YnabResponse> GetTransactions(string budgetId, string? startDate)
        {
            string resourceUrl = string.Format(this.configuration["YnabService:GetTransactionsResource"], budgetId);

            if (startDate is { })
            {
                resourceUrl += $"?since_date={startDate}";
            }

            var response = await this.httpClient.GetAsync(resourceUrl);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<YnabResponse>(responseStream);
        }
    }
}
