using System.Net.Http;

namespace InventoryManagementConsole
{
    public static class HttpClientFactory
    {
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7145/") 
        };

        public static HttpClient GetClient() => client;
    }
}
