namespace AIT.WebUIComponent.Models.Emails
{
    public class UserMessage : MessageModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}