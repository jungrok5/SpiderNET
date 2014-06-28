using Example.ErrorCode;
using Example.JsonNET;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerExample.Scripts.JsonNET
{
    public class JsonNETProtocolController : Controller
    {
        public string GET_KEY(Protocol_GET_KEY_REQ recvData)
        {
            Protocol_GET_KEY_ACK sendData = new Protocol_GET_KEY_ACK();
            sendData.nErrorCode = ErrorCode_GET_KEY.Success;
            sendData.szKey = MvcApplication.EncryptKey;

            return JsonConvert.SerializeObject(sendData);
        }

        public string LOGIN(Protocol_LOGIN_REQ recvData)
        {
            Protocol_LOGIN_ACK sendData = new Protocol_LOGIN_ACK();
            sendData.nErrorCode = ErrorCode_LOGIN.Success;
            sendData.biUserID = 100000001;
            sendData.szUserName = "TestUser";

            return JsonConvert.SerializeObject(sendData);
        }
    }
}
