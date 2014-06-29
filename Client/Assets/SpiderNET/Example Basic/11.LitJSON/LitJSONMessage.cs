using LitJson;
using SpiderNET.Core;
using System;
using System.Collections;
using System.Text;
using UnityEngine;

// Json.NET
// http://james.newtonking.com/json

namespace Example.LitJSON
{
    public class LitJSONMessage : Message<JsonData>
    {
        public LitJSONMessage(string id)
            : base(id)
        {
            ContentType = "application/json";
        }

        protected override ArraySegment<byte> Encode()
        {
            if (Data == null)
                return new ArraySegment<byte>();
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonMapper.ToJson(Data)));
        }

        protected override JsonData Decode(ArraySegment<byte> data)
        {
            return JsonMapper.ToObject(Encoding.UTF8.GetString(data.Array, data.Offset, data.Count));
        }

        public void AddField(string key, bool value)
        {
            if (Data == null)
                Data = new JsonData();
            Data[key] = new JsonData(value);
        }
        public void AddField(string key, double value)
        {
            if (Data == null)
                Data = new JsonData();
            Data[key] = new JsonData(value);
        }
        public void AddField(string key, int value)
        {
            if (Data == null)
                Data = new JsonData();
            Data[key] = new JsonData(value);
        }
        public void AddField(string key, long value)
        {
            if (Data == null)
                Data = new JsonData();
            Data[key] = new JsonData(value);
        }
        public void AddField(string key, object value)
        {
            if (Data == null)
                Data = new JsonData();
            Data[key] = new JsonData(value);
        }
        public void AddField(string key, string value)
        {
            if (Data == null)
                Data = new JsonData();
            Data[key] = new JsonData(value);
        }
    }
}