using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PTrampert.HttpClientLogger
{
    public class RequestLogMessage
    {
        public Uri Uri { get; set; }
        public string HttpMethod { get; set; }
        public IDictionary<string, IEnumerable<string>> Headers { get; set; } = new Dictionary<string, IEnumerable<string>>();
        public string Content { get; set; }
    }
}