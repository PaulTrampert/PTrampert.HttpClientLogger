using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PTrampert.HttpClientLogger.Test
{
    public class MockHandler : HttpMessageHandler
    {
        public HttpRequestMessage LastRequest { get; private set; }
        public HttpResponseMessage NextResponse { get; set; } = new HttpResponseMessage(HttpStatusCode.NoContent);


        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            return Task.FromResult(NextResponse);
        }
    }
}