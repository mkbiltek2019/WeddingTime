using System.IO;
using System.Web.Mvc;

namespace AIT.WebUtilities.Helpers
{
    public static class MvcHelpers
    {
        public static string RenderPartialView(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
            controller.ViewData.Model = model;

            return RenderView(controller, viewResult);
        }

        public static string RenderView(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            var viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null);
            controller.ViewData.Model = model;

            return RenderView(controller, viewResult);
        }

        private static string RenderView(Controller controller, ViewEngineResult viewResult)
        {
            using (var writer = new StringWriter())
            {
                var view = viewResult.View;
                var viewContext = new ViewContext(controller.ControllerContext, view, controller.ViewData, controller.TempData, writer);
                view.Render(viewContext, writer);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, view);

                return writer.ToString();
            }
        }
    }
}
