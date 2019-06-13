using System;

namespace AIT.WebUIComponent.Models.Emails
{
    public abstract class MessageModel
    {
        public MessageModel()
        {
            ContentId = Guid.NewGuid().ToString().Replace("-", "");
        }

        public string ContentId { get; private set; }
        public string Name { get; set; }
    }
}