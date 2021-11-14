using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTrampert.HttpClientLogger
{
    public class HttpClientLoggerConfig
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        public PrivateFields PrivateFields { get; set; } = new PrivateFields();

        public PrivateHeaders PrivateHeaders { get; set; } = new PrivateHeaders();
    }
}
