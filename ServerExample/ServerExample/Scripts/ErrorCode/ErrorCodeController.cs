using Example.ErrorCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.ErrorCode
{
    public class ErrorCodeController : Controller
    {
        public string GET_KEY(string version)
        {
            return string.Format("errorcode={0}", ErrorCode_GET_KEY.NeedUpdate);
        }

        public string LOGIN(string udid, byte platform)
        {
            return string.Format("errorcode={0}", ErrorCode_LOGIN.WrongUDID);
        }
    }
}
