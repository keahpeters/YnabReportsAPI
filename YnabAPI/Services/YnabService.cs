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
        Task<IEnumerable<YnabTransaction>> GetTransactions(string budgetId, DateTime? startDate);
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
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer {this.configuration["YnabService:AccessToken"]}");
        }

        public async Task<IEnumerable<YnabTransaction>> GetTransactions(string budgetId, DateTime? startDate)
        {
            string resourceUrl = string.Format(this.configuration["YnabService:GetTransactionsResource"], budgetId);

            var response = await this.httpClient.GetAsync(resourceUrl);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<YnabTransaction>>(responseStream);
        }
    }
}
