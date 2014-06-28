using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;

namespace Example.LitJSON
{
    public partial class Main : MonoBehaviour
    {
        void On_GET_KEY(JsonDotNetMessage message)
        {
            object recvData = message.Decode();
            Debug.Log(recvData);
            Send_LOGIN(SystemInfo.deviceUniqueIdentifier, (byte)Application.platform);
        }

        void On_LOGIN(JsonDotNetMessage message)
        {
            object recvData = message.Decode();
            Debug.Log(recvData);
        }

        void On_GET_KEY_USE_PROTOCOL(JsonDotNetMessage message)
        {
            Protocol_GET_KEY_ACK recvData = message.Decode<Protocol_GET_KEY_ACK>();
            Debug.Log("recvData.nErrorCode:" + recvData.nErrorCode);
            Debug.Log("recvData.szKey:" + recvData.szKey);

            Protocol_LOGIN_REQ sendData = new Protocol_LOGIN_REQ();
            sendData.szUdid = SystemInfo.deviceUniqueIdentifier;
            sendData.bPlatform = (byte)Application.platform;
            Send_LOGIN_USE_PROTOCOL(sendData);
        }

        void On_LOGIN_USE_PROTOCOL(JsonDotNetMessage message)
        {
            Protocol_LOGIN_ACK recvData = message.Decode<Protocol_LOGIN_ACK>();
            Debug.Log("recvData.nErrorCode:" + recvData.nErrorCode);
            Debug.Log("recvData.biUserID:" + recvData.biUserID);
            Debug.Log("recvData.szUserName:" + recvData.szUserName);
        }
    }
}