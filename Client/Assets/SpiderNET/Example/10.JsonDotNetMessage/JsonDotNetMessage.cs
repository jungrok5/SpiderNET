using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using UnityEngine;

// http://james.newtonking.com/json

namespace SpiderNET.Core
{
    public class JsonDotNetMessage : Message<object>
    {
        public ArraySegment<byte> NotDecodeRawData;

        public JsonDotNetMessage(string id)
            : base(id)
        {
            ContentType = "application/json";
        }

        public override ArraySegment<byte> RawData
        {
            get { return Encode(); }
            set { NotDecodeRawData = value; }
        }

        protected override ArraySegment<byte> Encode()
        {
            if (Data == null)
                return new ArraySegment<byte>();
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Data)));
        }

        protected override object Decode(ArraySegment<byte> data)
        {
            return null;
        }

        public T Decode<T>()
        {
            if (NotDecodeRawData.Array == null)
                return default(T);
            return JsonConvert.DeserializeObject<T>(
                Encoding.UTF8.GetString(
                NotDecodeRawData.Array, 
                NotDecodeRawData.Offset, 
                NotDecodeRawData.Count));
        }
    }
}