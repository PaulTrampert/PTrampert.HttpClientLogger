using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace PTrampert.HttpClientLogger.Test.HttpClientLoggerTests
{
    public abstract class WithHttpClientLogger
    {
        protected HttpClientLogger Subject { get; set; }
        protected Mock<ILogger> Logger { get; set; }
        protected MockHandler InnerHandler { get; set; }
        protected HttpClient HttpClient { get; set; }
        protected Mock<IOptions<HttpClientLoggerConfig>> Options { get; set; }
        protected HttpClientLoggerConfig Config { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            Logger = new Mock<ILogger>();
            InnerHandler = new MockHandler();
            Config = new HttpClientLoggerConfig();
            Options = new Mock<IOptions<HttpClientLoggerConfig>>();
            Options.SetupGet(o => o.Value).Returns(Config);
            Subject = new HttpClientLogger(Logger.Object, Options.Object, InnerHandler);
            HttpClient = new HttpClient(Subject);
        }
    }
}