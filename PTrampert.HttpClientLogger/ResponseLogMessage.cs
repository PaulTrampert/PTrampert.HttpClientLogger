using System.Collections.Generic;
using System.Net;

namespace PTrampert.HttpClientLogger
{
    public class ResponseLogMessage
    {
        public HttpStatusCode Status { get; internal set; }
        public string Content { get; internal set; }
        public IDictionary<string, IEnumerable<string>> Headers { get; internal set; }
    }
}