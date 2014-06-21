using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.Compression
{
    public class CompressionController : Controller
    {
        [CompressFilter]
        public string GET_KEY(string data)
        {
            return data;
        }

        [CompressFilter]
        public string LOGIN(string data)
        {
            return data;
        }
    }
}
