using AIT.GuestDomain.Services.RenewMembership.DTO;

namespace AIT.GuestDomain.Services.RenewMembership
{
    public interface IRenewMembershipService
    {
        bool Process(RenewSpecification specification);
    }
}