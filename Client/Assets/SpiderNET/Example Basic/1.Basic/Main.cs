using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System;

namespace Example.Basic
{
    public class Main : MonoBehaviour
    {
        private Session session;

        void Start()
        {
            GameObject go = new GameObject("Session");
            session = go.AddComponent<Session>();
            session.onSend += OnSend;
            session.onReceive += OnReceive;
            session.onError += OnError;
            session.Connect("localhost", 4623);

            SendExample1();
            SendExample2();
        }

        void SendExample1()
        {
            StringMessage message = new StringMessage("Basic/Example1?data=");
            message.Data = "Hello world";
            session.Send(message);
        }

        void SendExample2()
        {
            StringMessage message = new StringMessage("Basic/Example2");
            message.Data = "data=Hello world";
            session.Send(message);
        }

        void OnSend(string id)
        {
            Debug.Log(string.Format("[id:{0}] Send", id));
        }

        void OnReceive(string id, byte[] buffer, int offset, int length)
        {
            StringMessage recvMessage = new StringMessage(id);
            recvMessage.RawData = new ArraySegment<byte>(buffer, offset, length);

            Debug.Log(recvMessage.Data);

            Debug.Log(string.Format("[id:{0}] Recv", id));
        }

        void OnError(string id, string message, Exception e)
        {
            Debug.LogError(string.Format("[id:{0}] Error - {1} {2}", id, message, e != null ? e.Message : string.Empty));
        }
    }
}