using NLog;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AIT.WebUIComponent.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomHandleErrorAttribute : FilterAttribute, IExceptionFilter
    {
        private const string DefaultView = "Error";
        
        private Type _exceptionType = typeof(Exception);
        private Logger _log = LogManager.GetCurrentClassLogger();
        private string _master;
        private string _view;        
        
        public string Master
        {
            get { return _master ?? string.Empty; }
            set { _master = value; }
        }

        public string View
        {
            get { return (!string.IsNullOrEmpty(_view)) ? _view : DefaultView; }
            set { _view = value; }
        }

        public virtual void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (filterContext.IsChildAction)
                return;

            // if custom errors are disabled, we need to let the normal ASP.NET exception handler execute so that the user can see useful debugging information.
            if (filterContext.ExceptionHandled)     // || !filterContext.HttpContext.IsCustomErrorEnabled)
                return;

            Exception exception = filterContext.Exception;

            // if this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method), ignore it.
            if (new HttpException(null, exception).GetHttpCode() != 500)
                return;

            if (!_exceptionType.IsInstanceOfType(exception))
                return;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    //Data = new {},
                    ContentEncoding = Encoding.UTF8
                };                
            }
            else
            {
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];
                HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            _log.Error(exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            // certain versions of IIS will sometimes use their own error page when they detect a server error. Setting this property indicates that we want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}