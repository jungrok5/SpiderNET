using Example.ErrorCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.MiniJSON
{
    public class MiniJSONController : Controller
    {
        public JsonResult GET_KEY(string version)
        {
            return Json(new 
            { 
                version = version, 
                errorcode = ErrorCode_GET_KEY.NeedUpdate 
            });
        }

        public JsonResult LOGIN(string udid, byte platform)
        {
            return Json(new 
            { 
                udid = udid,
                platform = platform,
                errorcode = ErrorCode_LOGIN.NewUDID
            });
        }
    }
}
