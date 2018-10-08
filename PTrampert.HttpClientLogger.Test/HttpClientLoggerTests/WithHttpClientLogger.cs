using System.Net.Http;
using Microsoft.Extensions.Logging;
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

        [SetUp]
        public virtual void Setup()
        {
            Logger = new Mock<ILogger>();
            InnerHandler = new MockHandler();
            Subject = new HttpClientLogger(Logger.Object, InnerHandler);
            HttpClient = new HttpClient(Subject);
        }
    }
}