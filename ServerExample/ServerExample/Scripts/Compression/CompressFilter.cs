﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.Compression
{
    public class CompressFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(acceptEncoding)) 
                return;
            
            acceptEncoding = acceptEncoding.ToUpperInvariant();
            if (acceptEncoding != "GZIP") 
                return;

            HttpResponseBase response = filterContext.HttpContext.Response;
            response.AddHeader("Content-Encoding", "gzip");
            response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
        }
    }
}