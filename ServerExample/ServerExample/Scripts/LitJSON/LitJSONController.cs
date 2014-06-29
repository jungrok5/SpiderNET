using Example.ErrorCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.LitJSON
{
    public class LitJSONController : Controller
    {
        public JsonResult GET_KEY(string version)
        {
            return Json(new 
            { 
                errorcode = ErrorCode_GET_KEY.Success, 
                key = MvcApplication.EncryptKey 
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LOGIN(string udid, byte platform)
        {
            return Json(new
            {
                errorcode = ErrorCode_LOGIN.Success,
                userid = 100000001,
                username = "TestUser"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
