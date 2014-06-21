using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SpiderNET.Core
{
    public class Session : MonoBehaviour
    {
        protected static readonly string CONTENT_LENGTH = "Content-Length";
        protected static readonly string CONTENT_TYPE = "Content-Type";
        protected static readonly string URL = "http://{0}:{1}/{2}";

        public delegate void SendEvent(string id);
        public delegate void ReceiveEvent(string id, byte[] buffer, int offset, int length);
        public delegate void ErrorEvent(string id, string message, Exception e);
        public SendEvent onSend;
        public ReceiveEvent onReceive;
        public ErrorEvent onError;

        protected string RemoteAddress;
        protected int Port;
#if UNITY_4_5
        protected Dictionary<string, string> Headers;
#else
        protected Hashtable Headers;
#endif

        public virtual void Connect(string remoteAddress, int port)
        {
            RemoteAddress = remoteAddress;
            Port = port;
#if UNITY_4_5
            Headers = new Dictionary<string, string>();
#else
            Headers = new Hashtable();
#endif
        }

        public virtual void Send(IMessage message)
        {
            Headers[CONTENT_LENGTH] = message.RawData.Count.ToString();
            Headers[CONTENT_TYPE] = message.ContentType;
            StartCoroutine(ReceiveCallback(new WWW(string.Format(URL, RemoteAddress, Port, message.ID), message.RawData.Array, Headers), message.ID));

            OnSend(message.ID);
        }

        protected virtual IEnumerator ReceiveCallback(WWW www, string id)
        {
            yield return www;
            if (www.error == null)
                OnReceive(id, www.bytes, 0, www.bytes.Length);
            else
                OnError(id, www.error, null);
            www.Dispose();
        }

        protected virtual void OnSend(string id)
        {
            if (onSend != null)
                onSend(id);
        }

        protected virtual void OnReceive(string id, byte[] buffer, int offset, int length)
        {
            if (onReceive != null)
                onReceive(id, buffer, offset, length);
        }

        protected virtual void OnError(string id, string message, Exception e)
        {
            if (onError != null)
                onError(id, message, e);
        }
    }
}