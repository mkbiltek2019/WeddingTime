using AIT.GuestDomain.Services.RenewMembership.DTO;
using AIT.GuestDomain.Services.RenewMembership.Links;
using AIT.UtilitiesComponents.Chains;
using System;

namespace AIT.GuestDomain.Services.RenewMembership
{
    public class RenewMembershipService : ChainService<RenewSpecification, Boolean>, IRenewMembershipService
    {
        protected override IChainLink<RenewSpecification, bool> CreateChain()
        {
            var link = new RenewGroupAndMembershipLink();
            link.AddNextLink(new RenewGroupLink());
            link.Finally(new DoNothingChainLink<RenewSpecification, bool>());

            return link;
        }
    }
}