using System.Collections.Generic;

namespace AIT.UserDomain.Model.DTO
{
    public class OAuthClientData
    {
        public string ProviderName { get; private set; }
        public string DisplayName { get; private set; }
        public IDictionary<string, object> ExtraData { get; private set; }
    }
}
