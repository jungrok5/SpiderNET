using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SpiderNET.Core
{
    public interface IMessage
    {
        string ID { get; }
        string ContentType { get; }
        ArraySegment<byte> RawData { get; set; }
    }

    public interface IMessage<T> : IMessage
    {
        T Data { get; set; }
    }

    public abstract class Message<T> : IMessage<T>
    {
        public string ID { get; set; }
        public string ContentType { get; protected set; }
        public virtual ArraySegment<byte> RawData
        {
            get { return Encode(); }
            set { Data = Decode(value); }
        }

        public T Data { get; set; }

        public Message(string id) { ID = id; }
        protected abstract ArraySegment<byte> Encode();
        protected abstract T Decode(ArraySegment<byte> data);
    }
}