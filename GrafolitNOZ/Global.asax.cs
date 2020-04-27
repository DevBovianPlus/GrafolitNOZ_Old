using DatabaseWebService.Models;
using GrafolitNOZ.Infrastructure;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace GrafolitNOZ {
    public class Global_asax : System.Web.HttpApplication {
        void Application_Start(object sender, EventArgs e) {
            DevExpress.Web.ASPxWebControl.CallbackError += new EventHandler(Application_Error);
        }

        void Application_End(object sender, EventArgs e) {
            // Code that runs on application shutdown
        }
    
        void Application_Error(object sender, EventArgs e) {
            // Code that runs when an unhandled error occurs
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                UserModel serializeModel = serializer.Deserialize<UserModel>(authTicket.UserData);

                UserPrincipal userPrincipal = new UserPrincipal();

                userPrincipal.Identity = new GenericIdentity(authTicket.Name);
                userPrincipal.ID = serializeModel.ID;
                userPrincipal.firstName = serializeModel.firstName;
                userPrincipal.lastName = serializeModel.lastName;
                userPrincipal.email = serializeModel.email;
                userPrincipal.ProfileImage = serializeModel.profileImage;

                userPrincipal.LockedInquiryByUser = serializeModel.LockedInquiryByUser;
                userPrincipal.Signature = serializeModel.Signature;

                userPrincipal.Role = serializeModel.Role;
                userPrincipal.RoleId = serializeModel.RoleID;

                HttpContext.Current.User = userPrincipal;
            }
        }

        void Session_Start(object sender, EventArgs e) {
            // Code that runs when a new session is started
        }
    
        void Session_End(object sender, EventArgs e) {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}