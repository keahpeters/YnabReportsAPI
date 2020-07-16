using System;
using System.Net.Http;
using System.Net.Http.Headers;

using Microsoft.Extensions.Configuration;

namespace YnabAPI.Services
{
    public class YnabService
    {
        private readonly HttpClient httpClient;

        public YnabService(HttpClient httpClient, IConfiguration configuration)
        {
            httpClient.BaseAddress = new Uri(configuration["YnabService:BaseUrl"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer {configuration["YnabService:AccessToken"]}");

            this.httpClient = httpClient;
        }
    }
}
