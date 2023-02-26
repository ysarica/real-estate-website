using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRCBasicAdmin.Models.Arac
{
    public class UserAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["kullanici"] != null)
            {
                return true;
            }
            else
            {
                try
                {
                    httpContext.Response.Redirect("/Giris/Index");
                }
                catch
                {

                }

                return false;
            }

        }
    }
}