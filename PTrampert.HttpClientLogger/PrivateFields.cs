using System.Collections.Generic;

namespace PTrampert.HttpClientLogger
{
    public class PrivateFields : List<string>
    {
        public PrivateFields()
        {
            Add("password");
            Add("secret");
        }
    }
}