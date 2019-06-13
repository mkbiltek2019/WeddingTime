using System;
using System.Collections.Generic;

namespace AIT.UserDomain.Model.DTO
{
    public class OAuthResult
    {
        public Exception Error { get; private set; }
        public IDictionary<string, string> ExtraData { get; private set; }
        public bool IsSuccessful { get; private set; }
        public string Provider { get; private set; }
        public string ProviderUserId { get; private set; }
        public string UserName { get; private set; }
    }
}
