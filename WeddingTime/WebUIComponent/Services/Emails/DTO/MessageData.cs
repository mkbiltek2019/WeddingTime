using AIT.WebUIComponent.Models.Emails;

namespace AIT.WebUIComponent.Services.Emails.DTO
{
    public class MessageData
    {
        public string Destination { get; set; }
        public MessageModel Model { get; set; }
    }
}