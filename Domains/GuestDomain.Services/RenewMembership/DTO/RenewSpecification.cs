using AIT.GuestDomain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.GuestDomain.Services.RenewMembership.DTO
{
    public class RenewSpecification
    {
        public RenewItem RenewItem { get; set; }
        public Group Group { get; set; }
    }
}
