using System.Collections.Generic;

namespace PTrampert.HttpClientLogger
{
    public class PrivateHeaders : List<string>
    {
        public PrivateHeaders()
        {
            Add("Authorization");
        }
    }
}