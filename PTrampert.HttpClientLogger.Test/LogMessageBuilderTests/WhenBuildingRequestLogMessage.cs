using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace PTrampert.HttpClientLogger.Test.LogMessageBuilderTests
{
    public class WhenBuildingRequestLogMessage : WithLogMessageBuilder
    {
        [Test]
        public async Task ItRedactsPrivateContentFields()
        {
            var result = await Subject.BuildRequestLogMessage(
                new HttpRequestMessage(HttpMethod.Get, "https://example.com")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new {password = "herpderp"}), Encoding.UTF8,
                        "application/json")
                });
            Assert.That(result.Content.Contains(LogMessageBuilder.Redacted));
        }

        [Test]
        public async Task ItDoesntRedactRegularFields()
        {
            var result = await Subject.BuildRequestLogMessage(
                new HttpRequestMessage(HttpMethod.Get, "https://example.com")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { somedata = "herpderp" }), Encoding.UTF8,
                        "application/json")
                });
            Assert.That(result.Content.Contains("herpderp"));
        }

        [Test]
        public async Task ItRedactsSensitiveHeaders()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "abcd");
            var result = await Subject.BuildRequestLogMessage(request);

            Assert.That(result.Headers["Authorization"].First(), Is.EqualTo(LogMessageBuilder.Redacted));
        }

        [Test]
        public async Task ItDoesntRedactNonSensitiveHeaders()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");
            request.Headers.Add("X-Super-Awesome-Header", "somevalue");
            var result = await Subject.BuildRequestLogMessage(request);

            Assert.That(result.Headers["X-Super-Awesome-Header"].First(), Is.EqualTo("somevalue"));
        }
    }
}
