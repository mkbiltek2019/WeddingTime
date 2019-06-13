using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services.RenewMembership.DTO;
using AIT.UtilitiesComponents.Chains;
using System;
using System.Linq;

namespace AIT.GuestDomain.Services.RenewMembership.Links
{
    public class RenewGroupLink : ChainLink<RenewSpecification, Boolean>
    {
        //to jest przypadek, gdzie usunelismy wszystkich z grupy...wiec dla pierwszego elementu tworzymy tylko grupe
        public override bool CanHandle(RenewSpecification specification)
        {
            var renewItem = specification.RenewItem;
            var group = specification.Group;

            return renewItem.IsInnerGroupMember &&
                   !renewItem.RenewRelatedPersonGroupMembership &&
                   group.InnerGroups.All(n => n.GroupKey != renewItem.InnerGroupKey);
        }

        public override bool Handle(RenewSpecification specification)
        {
            if (CanHandle(specification))
            {
                specification.Group.AddInnerGroup(specification.RenewItem.InnerGroupKey.Value);

                return true;
            }

            return Successor.Handle(specification);
        }
    }
}