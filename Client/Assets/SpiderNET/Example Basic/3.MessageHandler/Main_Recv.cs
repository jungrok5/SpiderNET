using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;

namespace Example.MessageHandler
{
    public partial class Main : MonoBehaviour
    {
        void On_Example1(KeyValueMessage message)
        {
            foreach (var item in message.Data)
            {
                Debug.Log(string.Format("{0}={1}", item.Key, item.Value));
            }
        }

        void On_Example2(KeyValueMessage message)
        {
            foreach (var item in message.Data)
            {
                Debug.Log(string.Format("{0}={1}", item.Key, item.Value));
            }
        }
    }
}