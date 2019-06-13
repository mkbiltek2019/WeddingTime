using System;
using System.Web;
using System.Web.Optimization;

namespace AIT.WebUIComponent
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterLibsBundles(bundles);
            RegisterUtilsBundles(bundles);
            RegisterSharedBundles(bundles);
            RegisterLoginBundles(bundles);
            RegisterAutoLoginBundles(bundles);
            RegisterRegistrationBundles(bundles);
            RegisterNonAjaxInitFormBundles(bundles);
            RegisterGuestsBundles(bundles);
            RegisterExpensesBundles(bundles);
            RegisterTasksBundles(bundles);
            RegisterBallroomBundles(bundles);            

            RegisterStyleBundles(bundles);
            //BundleTable.EnableOptimizations = true;
        }

        private static void RegisterLibsBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                        "~/Scripts/Libs/jquery-{version}.js",
                        "~/Scripts/Libs/jquery-ui-{version}.js",
                        "~/Scripts/Libs/jquery.unobtrusive*",
                        "~/Scripts/Libs/jquery.validate*",
                        "~/Scripts/Libs/jquery.block-ui.js",
                        "~/Scripts/Libs/jquery.ui.touch-punch.js",
                        "~/Scripts/Libs/jsrender.js",
                        "~/Scripts/Libs/bootstrap.js",
                        "~/Scripts/Libs/unobtrusive-ext.js",
                        "~/Scripts/Libs/jquery.form-listener.js",
                        "~/Scripts/Libs/jquery.stick-to-bottom.js",
                        "~/Scripts/Libs/jquery.stick-to-top.js",
                        "~/Scripts/Libs/detect-mobile.js",
                        "~/Scripts/Libs/intro.js",
                        "~/Scripts/Libs/jquery.cookie.js",
                        "~/Scripts/Libs/jquery.countdown360.js"));
        }

        private static void RegisterUtilsBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/utils").Include(
                        "~/Scripts/Utils/requiredif-validation.js",
                        "~/Scripts/Utils/utilities.js",
                        "~/Scripts/Utils/tmpl-utils.js",
                        "~/Scripts/Utils/ajax-utils.js",
                        "~/Scripts/Utils/undo-ui.manager.js",
                        "~/Scripts/Utils/undo-utils.js",
                        "~/Scripts/Utils/error-utils.js",
                        "~/Scripts/Utils/dialog.animation.js",
                        "~/Scripts/Utils/form-utils.js",
                        "~/Scripts/Utils/countdown.manager.js"));
        }

        private static void RegisterSharedBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/shared").Include(
                        "~/Scripts/Shared/init.js",
                        "~/Scripts/Shared/shared.manager.js",
                        "~/Scripts/Shared/cookie.manager.js",
                        "~/Scripts/Shared/ui.manager.js"));
        }

        private static void RegisterLoginBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                        "~/Scripts/Account/login-init.js",
                        "~/Scripts/Account/login.manager.js"));
        }

        private static void RegisterAutoLoginBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/autologin").Include(
                       "~/Scripts/Account/auto-login-init.js",
                       "~/Scripts/Account/auto-login.manager.js"));
        }

        private static void RegisterRegistrationBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/register").Include(
                        "~/Scripts/Account/register.manager.js",
                        "~/Scripts/Account/register-init.js"));
        }

        private static void RegisterNonAjaxInitFormBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/enhance").Include(
                        "~/Scripts/Shared/non-ajax-form.init.js"));
        }

        private static void RegisterGuestsBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/guests").Include(
                        "~/Scripts/Guests/init.js",
                        "~/Scripts/Guests/members-info.manager.js",
                        "~/Scripts/Guests/sticky-items.manager.js",
                        "~/Scripts/Guests/filter.manager.js",
                        "~/Scripts/Guests/guests.service.js",
                        "~/Scripts/Guests/group.manager.js",
                        "~/Scripts/Guests/person.manager.js"));
        }

        private static void RegisterExpensesBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/expenses").Include(
                        "~/Scripts/Expenses/init.js",
                        "~/Scripts/Expenses/filter.manager.js",
                        "~/Scripts/Expenses/budget-info.manager.js",
                        "~/Scripts/Expenses/expenses.manager.js",
                        "~/Scripts/Expenses/expenses.service.js"));
        }

        private static void RegisterTasksBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/tasks").Include(
                       "~/Scripts/Tasks/init.js",
                       "~/Scripts/Tasks/sync.manager.js",
                       "~/Scripts/Tasks/card.manager.js",
                       "~/Scripts/Tasks/tasks.service.js",
                       "~/Scripts/Tasks/tasks.manager.js"));
        }

        private static void RegisterBallroomBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/ballroom").Include(
                        "~/Scripts/Ballroom/init.js",
                        "~/Scripts/Ballroom/ballroom.enums.js",
                        "~/Scripts/Ballroom/ballroom.service.js",
                        "~/Scripts/Ballroom/seat-utils.js",
                        "~/Scripts/Ballroom/seats.service.base.js",
                        "~/Scripts/Ballroom/seats-rect.service.js",
                        "~/Scripts/Ballroom/seats-round.service.js",
                        "~/Scripts/Ballroom/ballroom.manager.js",
                        "~/Scripts/Ballroom/ballroom-item.factory.js",
                        "~/Scripts/Ballroom/ballroom-seat.factory.js",
                        "~/Scripts/Ballroom/ballroom-intro.js"));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            // change path to images in site.css before releasing
            bundles.Add(new StyleBundle("~/Content/site/css").Include(
                        "~/Content/intro/introjs.css",
                        "~/Content/bootstrap/bootstrap.css",
                        "~/Content/bootstrap/bootstrap-theme.css",
                        "~/Content/site.css"));
        }
    }
}