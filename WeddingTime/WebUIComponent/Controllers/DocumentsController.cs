using System.Web.Mvc;

namespace AIT.WebUIComponent.Controllers
{
    [AllowAnonymous]
    public class DocumentsController : BaseController
    {             
        public ActionResult UserAgreement()
        {
            return View();
        }
        
        public ActionResult PrivacyAndCookiesPolicy()
        {
            return View();
        }

        public ActionResult GetUserAgreement()
        {
            return File("~/Content/regulations/regulamin.pdf", "application/pdf", Server.UrlEncode("Regulamin serwisu Zamiłowani.pl.pdf"));
        }

        public ActionResult GetPrivacyAndCookiesPolicy()
        {
            return File("~/Content/regulations/polityka_prywatnosci_i_plikow_cookies.pdf", "application/pdf", Server.UrlEncode("Polityka prywatności i plików cookies.pdf"));
        }

        public ActionResult GetWithdrawal()
        {
            return File("~/Content/regulations/odstapienie_od_umowy.pdf", "application/pdf", Server.UrlEncode("Formularz odstąpienia od umowy.pdf"));
        }
    }
}
