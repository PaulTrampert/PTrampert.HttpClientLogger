using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PTrampert.HttpClientLogger
{
    public class HttpClientLogger : DelegatingHandler
    {
        private ILogger log;
        private LogMessageBuilder builder;
        private HttpClientLoggerConfig config;

        public HttpClientLogger(ILoggerFactory logFactory, IOptionsSnapshot<HttpClientLoggerConfig> config, HttpMessageHandler innerHandler = null) 
            : this(logFactory.CreateLogger<HttpClientLogger>(), config, innerHandler) 
        {
        }
        
        public HttpClientLogger(ILogger log, IOptionsSnapshot<HttpClientLoggerConfig> config, HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        {
            this.log = log;
            this.config = config.Value;
            builder = new LogMessageBuilder(this.config.PrivateFields, this.config.PrivateHeaders);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                var requestLogMsg = builder.BuildRequestLogMessage(request);
                var responseLogMsg = builder.BuildResponseLogMessage(response);
                log.Log(config.LogLevel, "Request: {@Request} Response: {@Response}", requestLogMsg, responseLogMsg);
                return response;
            } 
            catch (Exception e)
            {
                log.LogError(e, "Error sending request: {@Request}", await builder.BuildRequestLogMessage(request));
                throw e;
            }
        }
    }
}