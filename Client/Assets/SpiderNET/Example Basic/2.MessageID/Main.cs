using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Example.MessageID
{
    public class Main : MonoBehaviour
    {
        private Session session;
        private MessageIDTable IDTable;

        void Start()
        {
            GameObject go = new GameObject("Session");
            session = go.AddComponent<Session>();
            session.onSend += OnSend;
            session.onReceive += OnReceive;
            session.onError += OnError;
            session.Connect("localhost", 4623);

            IDTable = new MessageIDTable();
            IDTable.AddID(MessageID.Example1, "MessageID/Example1");
            IDTable.AddID(MessageID.Example2, "MessageID/Example2");

            SendExample1();
            SendExample2();
        }

        void SendExample1()
        {
            KeyValueMessage message = new KeyValueMessage(IDTable[MessageID.Example1]);
            message.AddField("stringValue", "abcd");
            message.AddField("intValue", 1234);
            message.AddField("floatValue", 1234.5f);
            message.AddField("boolValue", true);
            session.Send(message);
        }

        public class Example2Data
        {
            public string stringValue;
            public int intValue;
            public float floatValue;
            public bool boolValue;
        }
        void SendExample2()
        {
            Example2Data data = new Example2Data();
            data.stringValue = "abcd";
            data.intValue = 1234;
            data.floatValue = 1234.5f;
            data.boolValue = true;          

            KeyValueMessage message = new KeyValueMessage(IDTable[MessageID.Example2]);
            foreach (var field in typeof(Example2Data).GetFields())
            {
                message.AddField(field.Name, field.GetValue(data));
            }
            session.Send(message);
        }

        void OnSend(string id)
        {
            Debug.Log(string.Format("[id:{0}] Send", IDTable[id]));
        }

        void OnReceive(string id, byte[] buffer, int offset, int length)
        {
            KeyValueMessage recvMessage = new KeyValueMessage(id);
            recvMessage.RawData = new ArraySegment<byte>(buffer, offset, length);

            foreach (var item in recvMessage.Data)
            {
                Debug.Log(string.Format("{0}={1}", item.Key, item.Value));
            }

            Debug.Log(string.Format("[id:{0}] Recv", IDTable[id]));
        }

        void OnError(string id, string message, Exception e)
        {
            Debug.LogError(string.Format("[id:{0}] Error - {1} {2}", IDTable[id], message, e != null ? e.Message : string.Empty));
        }
    }
}