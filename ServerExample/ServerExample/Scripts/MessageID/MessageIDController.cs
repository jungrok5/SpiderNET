using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.MessageID
{
    public class MessageIDController : Controller
    {
        public string Example1(string stringValue, int intValue, float floatValue, bool boolValue)
        {
            return string.Format("stringValue={0}&intValue={1}&floatValue={2}&boolValue={3}", 
                stringValue, intValue, floatValue, boolValue);
        }

        public class Example2Data
        {
            public string stringValue { get; set; }
            public int intValue { get; set; }
            public float floatValue { get; set; }
            public bool boolValue { get; set; }
        }
        public string Example2(Example2Data data)
        {
            return string.Join("&", data.GetType().GetProperties().Select(
                e => string.Format("{0}={1}", e.Name, e.GetValue(data)
                    )).ToArray());
        }
    }
}
