using DatabaseWebService.Models;
using GrafolitNOZ.Common;
using GrafolitNOZ.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace GrafolitNOZ.Infrastructure
{
    public class Authentication
    {
        private DatabaseConnection dbConnection;
        public Authentication()
        {
            dbConnection = new DatabaseConnection();
        }
        public bool Authenticate(string username, string password)
        {
            WebResponseContentModel<UserModel> user = null;
            CommonMethods.LogThis("Before SignIn");
            user = dbConnection.SignIn(username, password);
            CommonMethods.LogThis("After SignIn");

            if (user.Content != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string userData = serializer.Serialize(user.Content);

                string sessionExpires = ConfigurationManager.AppSettings["SessionTimeoutInMinutes"].ToString();

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     username,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(CommonMethods.ParseDouble(sessionExpires)),
                     false,
                     userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { HttpOnly = false, Expires = DateTime.Now.AddHours(CommonMethods.ParseDouble(sessionExpires)) };
                HttpContext.Current.Response.Cookies.Add(faCookie);

                InfrastructureHelper.SetCookieValue(Enums.Cookies.SessionExpires.ToString(), sessionExpires);
            }
            else
            {
                CommonMethods.LogThis(user.ValidationError);
                CommonMethods.LogThis(user.ValidationErrorAppSide);

                throw new Exception(user.ValidationError + " " + user.ValidationErrorAppSide);
            }
            return true;
        }
    }
}