using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace BasketApi.Tests
{
    public class TestClientFactory : IDisposable
    {
        private TestServer testServer;
        private HttpClient client;

        public HttpClient Create()
        {
            return client ?? (client = InstantiateClient());
        }

        private HttpClient InstantiateClient()
        {
            var webHostBuilder = new WebHostBuilder().UseStartup<Startup>();
            testServer = testServer ?? new TestServer(webHostBuilder);
            return testServer.CreateClient();
        }

        public void Dispose()
        {
            testServer?.Dispose();
            client?.Dispose();
        }
    }
}