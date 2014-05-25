using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.Basic
{
    public class BasicController : Controller
    {
        public string Example1()
        {
            byte[] data = new byte[HttpContext.Request.ContentLength];
            HttpContext.Request.InputStream.Read(data, 0, data.Length);
            return Encoding.UTF8.GetString(data);
        }

        public string Example2(string data)
        {
            return data;
        }
    }
}
