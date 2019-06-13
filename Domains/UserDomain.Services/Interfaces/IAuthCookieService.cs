using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.UserDomain.Services.Interfaces
{
    public interface IAuthCookieService
    {
        void CreateCookie(string userName);
        void SetCookieSecurityInformation();
    }
}
