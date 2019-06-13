using System.Globalization;

namespace AIT.UserDomain.Model.DTO
{
    public class OAuthUserAccount
    {
        public string Provider { get; set; }
        public string ProviderUserId { get; set; }

        public string ProviderDisplayName
        {
            get { return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Provider); }
        }
    }
}
