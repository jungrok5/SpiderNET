﻿using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;

namespace Example.MiniJSON
{
    public partial class Main : MonoBehaviour
    {
        void Send_GET_KEY(string version)
        {
            MiniJSONMessage message = new MiniJSONMessage(IDTable[MessageID.GET_KEY]);
            message.AddField("version", version);
            session.Send(message);
        }

        void Send_LOGIN(string udid, byte platform)
        {
            MiniJSONMessage message = new MiniJSONMessage(IDTable[MessageID.LOGIN]);
            message.AddField("udid", udid);
            message.AddField("platform", platform);
            session.Send(message);
        }
    }
}