using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.Encryption
{
    public class EncryptFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(acceptEncoding)) 
                return;
            
            acceptEncoding = acceptEncoding.ToUpperInvariant();
            if (acceptEncoding != "ENCRYPT") 
                return;

            HttpResponseBase response = filterContext.HttpContext.Response;
            response.AddHeader("Content-Encoding", "encrypt");
            response.Filter = new EncryptStream(response.Filter, MvcApplication.EncryptKey);
        }
    }
}