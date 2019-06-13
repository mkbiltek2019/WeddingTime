using System.ComponentModel;

namespace AIT.GuestDomain.Model.Enums
{
    public enum PersonGenre
    {
        [Description("Osoba dorosła")]
        Adult = 0,
        [Description("Dziecko")]
        Child = 1
    }
}
