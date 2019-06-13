using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AIT.UserDomain.Model.DTO
{
    public class CustomPrincipal : GenericPrincipal
    {
        public CustomPrincipal(IIdentity identity, Guid userKey)
            : base(identity, null)
        {
            UserKey = userKey;
        }

        public Guid UserKey { get; private set; }
    }
}
