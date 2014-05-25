using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;

namespace Example.MessageHandler
{
    public partial class Main : MonoBehaviour
    {
        void Send_Example1(string stringValue, int intValue, float floatValue, bool boolValue)
        {
            KeyValueMessage message = new KeyValueMessage(IDTable[MessageID.Example1]);
            message.AddField("stringValue", stringValue);
            message.AddField("intValue", intValue);
            message.AddField("floatValue", floatValue);
            message.AddField("boolValue", boolValue);
            session.Send(message);
        }

        void Send_Example2(string udid, byte platform)
        {
            KeyValueMessage message = new KeyValueMessage(IDTable[MessageID.Example2]);
            message.AddField("udid", udid);
            message.AddField("platform", platform);
            session.Send(message);
        }
    }
}