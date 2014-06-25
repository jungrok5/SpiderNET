using ServerExample.Scripts.Compression;
using ServerExample.Scripts.Encryption;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ServerExample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly string EncryptKey = "oirejrf2#R)EIQWEODJ!@#e0c1290312";
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AES.Init(EncryptKey);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string acceptEncoding = Request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(acceptEncoding))
                return;

            acceptEncoding = acceptEncoding.ToUpperInvariant();
            if (acceptEncoding.Contains("GZIP"))
            {
                Request.Filter = new GZipStream(Request.Filter, CompressionMode.Decompress);
            }
            else if (acceptEncoding.Contains("ENCRYPT"))
            {
                Request.Filter = new EncryptStream(Request.Filter, EncryptKey);
            }
        }
    }
}