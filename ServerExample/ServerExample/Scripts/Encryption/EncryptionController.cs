using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.Encryption
{
    public class EncryptionController : Controller
    {
        public string GET_KEY(string version)
        {
            return "key=" + MvcApplication.EncryptKey;
        }

        [EncryptFilter]
        public string LOGIN(string udid, byte platform)
        {
            return "udid=" + udid + "&platform=" + platform;
        }
    }
}
