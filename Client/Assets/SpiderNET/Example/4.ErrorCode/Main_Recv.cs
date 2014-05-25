using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;

namespace Example.ErrorCode
{
    public partial class Main : MonoBehaviour
    {
        void On_GET_KEY(KeyValueMessage message)
        {
            if (message.Data.ContainsKey("errorcode") == true)
            {
                Debug.Log(ErrorCode_GET_KEY.ToString(int.Parse(message.Data["errorcode"].ToString())));
            }
        }

        void On_LOGIN(KeyValueMessage message)
        {
            if (message.Data.ContainsKey("errorcode") == true)
            {
                Debug.Log(ErrorCode_LOGIN.ToString(int.Parse(message.Data["errorcode"].ToString())));
            }
        }
    }
}