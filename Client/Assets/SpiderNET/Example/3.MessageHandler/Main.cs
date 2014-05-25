using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Example.MessageHandler
{
    public partial class Main : MonoBehaviour
    {
        private Session session;
        private MessageIDTable IDTable;
        private Dictionary<MessageID, MethodInfo> HandlerFunctionTable;
        private readonly string HANDLER_FUNCTION_NAME = "On_{0}";

        void Start()
        {
            GameObject go = new GameObject("Session");
            session = go.AddComponent<Session>();
            session.onSend += OnSend;
            session.onReceive += OnReceive;
            session.onError += OnError;
            session.Connect("localhost", 4623);

            IDTable = new MessageIDTable();
            IDTable.AddID(MessageID.Example1, "MessageHandler/Example1");
            IDTable.AddID(MessageID.Example2, "MessageHandler/Example2");

            HandlerFunctionTable = new Dictionary<MessageID, MethodInfo>();
            foreach (var id in IDTable.IDs)
            {
                HandlerFunctionTable.Add(id, this.GetType().GetMethod(string.Format(HANDLER_FUNCTION_NAME, id), BindingFlags.NonPublic));
            }

            Send_Example1("abcd", 1234, 1234.5f, true);
            Send_Example2(SystemInfo.deviceUniqueIdentifier, (byte)Application.platform);
        }

        void MessageHandler_Normal(KeyValueMessage message)
        {
            MessageID id = IDTable[message.ID];
            switch (id)
            {
                case MessageID.Example1:
                    On_Example1(message);
                    break;
                case MessageID.Example2:
                    On_Example2(message);
                    break;
                default:
                    Debug.LogWarning(string.Format("Receive unknown message:{0}", id));
                    break;
            }
        }

        void MessageHandler_Reflection(KeyValueMessage message)
        {
            MessageID id = IDTable[message.ID];
            MethodInfo mi = HandlerFunctionTable[id];
            if (mi == null)
            {
                Debug.LogWarning(string.Format("Receive unknown message:{0}", id));
                return;
            }
            mi.Invoke(this, new object[] { message });
        }

        void OnSend(string id)
        {
            Debug.Log(string.Format("[id:{0}] Send", IDTable[id]));
        }

        void OnReceive(string id, byte[] buffer, int offset, int length)
        {
            Debug.Log(string.Format("[id:{0}] Recv", IDTable[id]));

            KeyValueMessage recvMessage = new KeyValueMessage(id);
            recvMessage.RawData = new ArraySegment<byte>(buffer, offset, length);

            MessageHandler_Normal(recvMessage);
            //MessageHandler_Reflection(recvMessage);
        }

        void OnError(string id, string message, Exception e)
        {
            Debug.LogError(string.Format("[id:{0}] Error - {1} {2}", IDTable[id], message, e != null ? e.Message : string.Empty));
        }
    }
}