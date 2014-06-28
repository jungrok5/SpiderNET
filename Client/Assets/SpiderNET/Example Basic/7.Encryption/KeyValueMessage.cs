using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpiderNET.Core;

namespace Example.Encryption
{
    public class KeyValueMessage : Message<Dictionary<string, object>>
    {
        private static readonly string KVP = "{0}={1}";

        public KeyValueMessage(string id)
            : base(id)
        {
            ContentType = "application/x-www-form-urlencoded";
        }

        protected override ArraySegment<byte> Encode()
        {
            if (Data == null)
                return new ArraySegment<byte>();
            return new ArraySegment<byte>(
                Encoding.UTF8.GetBytes(
                string.Join("&", Data.Select(e => string.Format(KVP, e.Key, e.Value)).ToArray())
                ));
        }

        protected override Dictionary<string, object> Decode(ArraySegment<byte> data)
        {
            Dictionary<string, object> decodeData = new Dictionary<string, object>();
            string stringData = Encoding.UTF8.GetString(data.Array, data.Offset, data.Count);
            foreach (var kvp in stringData.Split('&'))
            {
                string[] kvpData = kvp.Split('=');
                if (kvpData == null || kvpData.Length != 2)
                    continue;
                decodeData.Add(kvpData[0], kvpData[1]);
            }
            return decodeData;
        }

        public void AddField(string key, object value)
        {
            if (Data == null)
                Data = new Dictionary<string, object>();
            Data.Add(key, value);
        }
    }
}