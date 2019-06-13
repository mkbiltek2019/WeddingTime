using Microsoft.AspNet.Identity;
using NLog;
using System.Web.Mvc;
using System.Web.SessionState;

namespace AIT.WebUIComponent.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public abstract class BaseController : Controller
    {        
        protected BaseController()
        {
            Log = LogManager.GetLogger(GetType().FullName);
        }

        protected Logger Log { get; private set; }

        protected string UserId
        {
            get { return User.Identity.GetUserId(); }
        }        
    }
}
