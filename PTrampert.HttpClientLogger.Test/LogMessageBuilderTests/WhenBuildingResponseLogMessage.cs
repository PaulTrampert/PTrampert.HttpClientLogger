using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PTrampert.HttpClientLogger.Test.LogMessageBuilderTests
{
    public class WhenBuildingResponseLogMessage : WithLogMessageBuilder
    {
        protected HttpResponseMessage response;

        public override void SetUp()
        {
            base.SetUp();
            response = new HttpResponseMessage();
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.InternalServerError)]
        public async Task ItSetsStatusCode(HttpStatusCode code)
        {
            response.StatusCode = code;
            var result = await Subject.BuildResponseLogMessage(response);
            Assert.That(result.Status, Is.EqualTo(code));
        }
    }
}
