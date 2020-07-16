using System;
using System.Net.Http;

using Microsoft.Extensions.Configuration;

namespace YnabAPI.Services
{
    public class YnabService
    {
        private readonly HttpClient httpClient;

        public YnabService(HttpClient httpClient, IConfiguration configuration)
        {
            httpClient.BaseAddress = new Uri(configuration["YnabService:BaseUrl"]);

            this.httpClient = httpClient;
        }
    }
}
