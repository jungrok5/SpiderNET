using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.MessageHandler
{
    public class MessageHandlerController : Controller
    {
        public string Example1(string stringValue, int intValue, float floatValue, bool boolValue)
        {
            return "errorcode=success&stringValue=" + stringValue;
        }

        public string Example2(string udid, byte platform)
        {
            return "errorcode=success&udid=" + udid;
        }
    }
}
