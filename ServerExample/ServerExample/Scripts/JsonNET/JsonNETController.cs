using Example.ErrorCode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.JsonNET
{
    public class JsonNETController : Controller
    {
        public string GET_KEY(string version)
        {
            return JsonConvert.SerializeObject(new
            {
                version = version,
                errorcode = ErrorCode_GET_KEY.NeedUpdate
            });
        }

        public string LOGIN(string udid, byte platform)
        {
            return JsonConvert.SerializeObject(new
            {
                udid = udid,
                platform = platform,
                errorcode = ErrorCode_LOGIN.NewUDID
            });
        }
    }
}
