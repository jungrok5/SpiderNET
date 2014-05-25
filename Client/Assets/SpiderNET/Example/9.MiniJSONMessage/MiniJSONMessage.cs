using System;
using System.Collections;
using System.Text;
using UnityEngine;

// https://gist.github.com/darktable/1411710

namespace SpiderNET.Core
{
    public class MiniJSONMessage : Message<Hashtable>
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
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes(MiniJSON.Json.Serialize(Data)));
        }

        protected override Hashtable Decode(ArraySegment<byte> data)
        {
            return (Hashtable)MiniJSON.Json.Deserialize(Encoding.UTF8.GetString(data.Array, data.Offset, data.Count));
        }

        public void AddField(string key, object value)
        {
            if (Data == null)
                Data = new Hashtable();
            Data.Add(key, value);
        }
    }
}