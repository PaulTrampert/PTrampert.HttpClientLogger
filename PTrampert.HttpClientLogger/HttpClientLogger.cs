using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PTrampert.HttpClientLogger
{
    public class HttpClientLogger : DelegatingHandler
    {
        private ILogger log;

        public HttpClientLogger(ILoggerFactory logFactory, HttpMessageHandler innerHandler = null) 
            : this(logFactory.CreateLogger<HttpClientLogger>(), innerHandler) 
        {
        }
        
        public HttpClientLogger(ILogger log, HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        {
            this.log = log;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            var response = await base.SendAsync(request, cancellationToken);
            log.LogInformation("Got HTTP Response");
            return response;
        }
    }
}