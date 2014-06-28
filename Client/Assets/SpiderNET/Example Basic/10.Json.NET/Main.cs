using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Example.JsonNET
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
            IDTable.AddID(MessageID.GET_KEY, "JsonNET/GET_KEY");
            IDTable.AddID(MessageID.LOGIN, "JsonNET/LOGIN");
            IDTable.AddID(MessageID.GET_KEY_USE_PROTOCOL, "JsonNETProtocol/GET_KEY");
            IDTable.AddID(MessageID.LOGIN_USE_PROTOCOL, "JsonNETProtocol/LOGIN");

            HandlerFunctionTable = new Dictionary<MessageID, MethodInfo>();
            foreach (var id in IDTable.IDs)
            {
                HandlerFunctionTable.Add(id, this.GetType().GetMethod(string.Format(HANDLER_FUNCTION_NAME, id), BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance));
            }

            // 무명 클래스 이용
            //Send_GET_KEY("0.0.1");

            // 미리 정의된 프로토콜 클래스 이용
            Protocol_GET_KEY_REQ sendData = new Protocol_GET_KEY_REQ();
            sendData.szVersion = "0.0.1";
            Send_GET_KEY_USE_PROTOCOL(sendData);
        }

        void MessageHandler_Normal(JsonDotNetMessage message)
        {
            MessageID id = IDTable[message.ID];
            switch (id)
            {
                case MessageID.GET_KEY:
                    On_GET_KEY(message);
                    break;
                case MessageID.LOGIN:
                    On_LOGIN(message);
                    break;
                case MessageID.GET_KEY_USE_PROTOCOL:
                    On_GET_KEY_USE_PROTOCOL(message);
                    break;
                case MessageID.LOGIN_USE_PROTOCOL:
                    On_LOGIN_USE_PROTOCOL(message);
                    break;
                default:
                    Debug.LogWarning(string.Format("Receive unknown message:{0}", id));
                    break;
            }
        }

        void MessageHandler_Reflection(JsonDotNetMessage message)
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

            JsonDotNetMessage recvMessage = new JsonDotNetMessage(id);
            recvMessage.RawData = new ArraySegment<byte>(buffer, offset, length);

            //MessageHandler_Normal(recvMessage);
            MessageHandler_Reflection(recvMessage);
        }

        void OnError(string id, string message, Exception e)
        {
            Debug.LogError(string.Format("[id:{0}] Error - {1} {2}", IDTable[id], message, e != null ? e.Message : string.Empty));
        }
    }
}