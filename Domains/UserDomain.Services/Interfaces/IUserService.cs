using AIT.UserDomain.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.UserDomain.Services.Interfaces
{
    public interface IUserService
    {
        int GetUserId(Guid userKey);
    }
}
