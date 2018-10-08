using System.Linq;
using System.Net.Http;
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
    }
}