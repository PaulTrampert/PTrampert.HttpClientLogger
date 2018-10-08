using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PTrampert.HttpClientLogger.Test.HttpClientLoggerTests
{
    public class WhenSendingAsync : WithHttpClientLogger
    {
        [Test]
        public async Task ItPassesTheRequestToTheInnerHandler()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");
            await HttpClient.SendAsync(request);
            Assert.That(InnerHandler.LastRequest, Is.SameAs(request));
        }

        [Test]
        public async Task ItReturnsTheResponseFromTheInnerHandler()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");
            var response = await HttpClient.SendAsync(request);
            Assert.That(InnerHandler.NextResponse, Is.SameAs(response));
        }
    }
}