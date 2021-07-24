using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace FPTCourse_ASP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var AccountCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (AccountCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(AccountCookie.Value);
                var Permission_ID = authTicket.UserData.Split(new char[] {','});
                var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), Permission_ID);
                Context.User = userPrincipal;
            }
        }
    }
   
}
