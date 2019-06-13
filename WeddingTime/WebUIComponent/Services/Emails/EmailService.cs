using AIT.UtilitiesComponents.Strategy;
using AIT.WebUIComponent.Models.Emails;
using AIT.WebUIComponent.Services.Emails.DTO;
using AIT.WebUIComponent.Services.Emails.Enum;
using RazorEngine.Templating;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AIT.WebUIComponent.Services.Emails
{
    public class EmailService : IEmailService
    {
        private IRazorEngineService _engineService;
        private IFunctionStrategyService<EmailType, MessageData, Task> _emailStrategy;

        public EmailService()
        {
            _engineService = RazorEngineService.Create();
            InitializeEmailStrategy();
        }

        public Task SendAsync(EmailType type, MessageModel model)
        {
            return SendAsync(type, model, "kontakt@zamilowani.pl");
        }

        public Task SendAsync(EmailType type, MessageModel model, string destination)
        {
            var messageData = new MessageData { Model = model, Destination = destination };
            return _emailStrategy.Execute(type, messageData);
        }

        private void InitializeEmailStrategy()
        {
            _emailStrategy = new FunctionStrategyService<EmailType, MessageData, Task>();
            _emailStrategy.AddStrategy(EmailType.ConfirmAccount, SendConfirmAccountMessage);
            _emailStrategy.AddStrategy(EmailType.ChangeEmail, SendChangeEmailMessage);
            _emailStrategy.AddStrategy(EmailType.ResetPassword, SendResetPasswordMessage);
            _emailStrategy.AddStrategy(EmailType.UserMessage, SendMessageFromUser);
        }

        private Task SendChangeEmailMessage(MessageData data)
        {
            return PrepareAndSendMailAsync<SystemMessage>(data, "ConfirmChangedEmail", "Potwierdź adres e-mail");            
        }

        private Task SendConfirmAccountMessage(MessageData data)
        {
            return PrepareAndSendMailAsync<SystemMessage>(data, "ConfirmAccountEmail", "Potwierdź swoje konto");            
        }

        private Task SendResetPasswordMessage(MessageData data)
        {
            return PrepareAndSendMailAsync<SystemMessage>(data, "ResetPasswordEmail", "Ustaw nowe hasło");
        }

        private Task SendMessageFromUser(MessageData data)
        {
            return PrepareAndSendMailAsync<UserMessage>(data, "FeedbackEmail", ((UserMessage)data.Model).Subject);
        }

        private Task PrepareAndSendMailAsync<TMessage>(MessageData data, string templateName, string subject) where TMessage : MessageModel
        {
            string template = ReadEmailTemplate(templateName);
            string body = _engineService.RunCompile(template, templateName, null, data.Model as TMessage);

            AlternateView alternateView = CreateAlternateView(body, data.Model.ContentId);

            return SendMailAsync(subject, body, data.Destination, alternateView);
        }

        private string ReadEmailTemplate(string templateName)
        {
            return File.ReadAllText(HttpContext.Current.Server.MapPath(string.Format("~/Services/Emails/Templates/{0}.cshtml", templateName)));
        }
        
        private AlternateView CreateAlternateView(string body, string contentId)
        {
            var imageLink = CreateImgLink(contentId);

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, "text/html");
            alternateView.LinkedResources.Add(imageLink);

            return alternateView;
        }

        private LinkedResource CreateImgLink(string contentId)
        {
            var imgPath = HttpContext.Current.Server.MapPath("~/Images/logo.png");
            return new LinkedResource(imgPath, "image/png")
            {
                ContentId = contentId,
                TransferEncoding = TransferEncoding.Base64
            };
        }

        private async Task SendMailAsync(string subject, string body, string destination, AlternateView alternateView)
        {
            var email = new MailMessage(new MailAddress("no-reply@zamilowani.pl", "Zamiłowani.pl"),
                                        new MailAddress(destination))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            email.AlternateViews.Add(alternateView);

            using (var client = new SmtpClient())
            {
                // use for instance smtp4dev
                await client.SendMailAsync(email);
            }
        }
    }
}