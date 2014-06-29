using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;

namespace Example.LitJSON
{
    public partial class Main : MonoBehaviour
    {
        void On_GET_KEY(LitJSONMessage message)
        {
            Debug.Log(message.Data["errorcode"].ToString());
            Debug.Log(message.Data["key"].ToString());

            Send_LOGIN(SystemInfo.deviceUniqueIdentifier, (byte)Application.platform);
        }

        void On_LOGIN(LitJSONMessage message)
        {
            Debug.Log(message.Data["errorcode"].ToString());
            Debug.Log(message.Data["userid"].ToString());
            Debug.Log(message.Data["username"].ToString());
        }
    }
}