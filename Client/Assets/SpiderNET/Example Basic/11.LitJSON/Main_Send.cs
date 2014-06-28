using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;

namespace Example.LitJSON
{
    public partial class Main : MonoBehaviour
    {
        void Send_GET_KEY(string version)
        {
            JsonDotNetMessage message = new JsonDotNetMessage(IDTable[MessageID.GET_KEY]);
            message.Data = new { version = version };
            session.Send(message);
        }

        void Send_LOGIN(string udid, byte platform)
        {
            JsonDotNetMessage message = new JsonDotNetMessage(IDTable[MessageID.LOGIN]);
            message.Data = new { udid = udid, platform = platform };
            session.Send(message);
        }

        void Send_GET_KEY_USE_PROTOCOL(Protocol_GET_KEY_REQ sendData)
        {
            JsonDotNetMessage message = new JsonDotNetMessage(IDTable[MessageID.GET_KEY_USE_PROTOCOL]);
            message.Data = sendData;
            session.Send(message);
        }

        void Send_LOGIN_USE_PROTOCOL(Protocol_LOGIN_REQ sendData)
        {
            JsonDotNetMessage message = new JsonDotNetMessage(IDTable[MessageID.LOGIN_USE_PROTOCOL]);
            message.Data = sendData;
            session.Send(message);
        }
    }
}