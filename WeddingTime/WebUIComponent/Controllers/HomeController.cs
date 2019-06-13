using AIT.WebUIComponent.Models.Emails;
using AIT.WebUIComponent.Models.Home;
using AIT.WebUIComponent.Services.Emails;
using AIT.WebUIComponent.Services.Emails.Enum;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AIT.WebUIComponent.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IEmailService _emailService;

        public HomeController(ApplicationUserManager userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<ActionResult> Contact(string message)
        {
            ViewBag.StatusMessage = message == null ? string.Empty : message;

            var model = new ContactModel();

            if (Request.IsAuthenticated)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                model.Email = user.Email;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userMessage = new UserMessage
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Body = model.Body
            };

            await _emailService.SendAsync(EmailType.UserMessage, userMessage);

            return RedirectToAction("Contact", new { Message = "Wiadmość została wysłana." });
        }        
    }
}
