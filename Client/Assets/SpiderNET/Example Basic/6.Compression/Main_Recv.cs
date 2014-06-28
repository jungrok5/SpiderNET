using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;

namespace Example.Compression
{
    public partial class Main : MonoBehaviour
    {
        void On_GET_KEY(KeyValueMessage message)
        {
            if (message.Data.ContainsKey("errorcode") == true)
            {
                Debug.Log(ErrorCode_GET_KEY.ToString(int.Parse(message.Data["errorcode"].ToString())));
            }

            foreach (var kvp in message.Data)
            {
                Debug.Log(string.Format("{0}={1}", kvp.Key, kvp.Value));
            }
        }

        void On_LOGIN(KeyValueMessage message)
        {
            if (message.Data.ContainsKey("errorcode") == true)
            {
                Debug.Log(ErrorCode_LOGIN.ToString(int.Parse(message.Data["errorcode"].ToString())));
            }

            foreach (var kvp in message.Data)
            {
                Debug.Log(string.Format("{0}={1}", kvp.Key, kvp.Value));
            }
        }
    }
}