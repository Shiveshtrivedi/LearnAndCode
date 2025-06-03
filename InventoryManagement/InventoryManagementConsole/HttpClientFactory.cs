using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace InventoryManagementConsole
{
    public  class HttpClientFactory : IHttpClientFactoryWrapper
    {
        private readonly HttpClient client;

        public HttpClientFactory(IConfiguration configuration)
        {
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("BaseUrl is missing in configuration.");

            client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public HttpClient GetClient() => client;
    }
}
