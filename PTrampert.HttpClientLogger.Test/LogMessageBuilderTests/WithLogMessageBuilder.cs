using NUnit.Framework;

namespace PTrampert.HttpClientLogger.Test.LogMessageBuilderTests
{
    public class WithLogMessageBuilder
    {
        protected LogMessageBuilder Subject { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            Subject = new LogMessageBuilder();
        }
    }
}