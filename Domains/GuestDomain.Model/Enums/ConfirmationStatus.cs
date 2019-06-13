using System.ComponentModel;

namespace AIT.GuestDomain.Model.Enums
{
    public enum ConfirmationStatus
    {
        [Description("Niepotwierdzony")]
        Unconfirmed = 0,
        [Description("Zaakceptowany")]
        Accepted = 1,
        [Description("Odmówiony")]
        Declined = 2
    }
}
