using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PTrampert.HttpClientLogger
{
    public class LogMessageBuilder
    {
        public const string Redacted = "***REDACTED***";
        private readonly PrivateFields privateFields;
        private readonly PrivateHeaders privateHeaders;

        public LogMessageBuilder(PrivateFields privateFields, PrivateHeaders privateHeaders)
        {
            this.privateFields = privateFields;
            this.privateHeaders = privateHeaders;
        }

        public LogMessageBuilder() :
            this(new PrivateFields(), new PrivateHeaders())
        {
        }

        public async Task<RequestLogMessage> BuildRequestLogMessage(HttpRequestMessage request)
        {
            return new RequestLogMessage
            {
                Uri = request.RequestUri,
                HttpMethod = request.Method.ToString(),
                Headers = BuildSanitizedHeaders(request.Headers),
                Content = await BuildSanitizedContent(request.Content)
            };
        }

        private async Task<string> BuildSanitizedContent(HttpContent content)
        {
            if (content == null) return null;
            var result = "Non-Loggable Content"; 
            switch (content.Headers.ContentType.MediaType)
            {
                case "application/json":
                    result = await content.ReadAsStringAsync();
                    foreach (var field in privateFields)
                    {
                        result = Regex.Replace(result, $@"(""{field}""\s*:\s*"")(\\""|[^""])*("")", $"$1{Redacted}$3", RegexOptions.IgnoreCase);
                    }

                    break;
                case "application/x-www-form-urlencoded":
                    result = await content.ReadAsStringAsync();
                    foreach (var field in privateFields)
                    {
                        result = Regex.Replace(result, $@"((^|[?&\.]){field}=)([^&]*)", $"$1$2{Redacted}", RegexOptions.IgnoreCase);
                    }
                    break;
            }

            return result;
        }

        private IDictionary<string, IEnumerable<string>> BuildSanitizedHeaders(HttpHeaders headers)
        {
            var result = new Dictionary<string, IEnumerable<string>>();
            foreach (var header in headers)
            {
                if (!result.ContainsKey(header.Key))
                {
                    result.Add(header.Key, new List<string>());
                }

                if (privateHeaders.Contains(header.Key))
                {
                    result[header.Key] = new [] {Redacted};
                }
                else
                {
                    result[header.Key] = header.Value;
                }
            }

            return result;
        }
    }
}