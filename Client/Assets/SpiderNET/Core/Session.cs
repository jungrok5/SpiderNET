using UnityEngine;
using System.Collections;
using System;

namespace SpiderNET.Core
{
    public class Session : MonoBehaviour
    {
        private static readonly string CONTENT_LENGTH = "Content-Length";
        private static readonly string CONTENT_TYPE = "Content-Type";
        private static readonly string URL = "http://{0}:{1}/{2}";

        public delegate void SendEvent(string id);
        public delegate void ReceiveEvent(string id, byte[] buffer, int offset, int length);
        public delegate void ErrorEvent(string id, string message, Exception e);
        public SendEvent onSend;
        public ReceiveEvent onReceive;
        public ErrorEvent onError;

        protected string RemoteAddress;
        protected int Port;
        protected Hashtable Headers;

        public virtual void Connect(string remoteAddress, int port)
        {
            RemoteAddress = remoteAddress;
            Port = port;
            Headers = new Hashtable();
        }
        
        public virtual void Send(IMessage message)
        {
            Headers[CONTENT_LENGTH] = message.RawData.Count;
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