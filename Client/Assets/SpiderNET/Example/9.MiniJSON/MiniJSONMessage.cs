using MiniJSON;
using SpiderNET.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// MiniJSON
// https://gist.github.com/darktable/1411710

namespace Example.MiniJSON
{
    public class MiniJSONMessage : Message<Dictionary<string, object>>
    {
        public MiniJSONMessage(string id)
            : base(id)
        {
            ContentType = "application/json";
        }

        protected override ArraySegment<byte> Encode()
        {
            if (Data == null)
                return new ArraySegment<byte>();
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes(Json.Serialize(Data)));
        }

        protected override Dictionary<string, object> Decode(ArraySegment<byte> data)
        {
            return (Dictionary<string, object>)Json.Deserialize(Encoding.UTF8.GetString(data.Array, data.Offset, data.Count));
        }

        public void AddField(string key, object value)
        {
            if (Data == null)
                Data = new Dictionary<string, object>();
            Data.Add(key, value);
        }
    }
}