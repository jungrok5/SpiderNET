using System;
using System.Text;
using SpiderNET.Core;

namespace Example.Basic
{
    public class StringMessage : Message<string>
    {
        public StringMessage(string id)
            : base(id)
        {
            ContentType = "application/x-www-form-urlencoded";
        }

        protected override ArraySegment<byte> Encode()
        {
            if (Data == null)
                return new ArraySegment<byte>();
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes(Data));
        }

        protected override string Decode(ArraySegment<byte> data)
        {
            return Encoding.UTF8.GetString(data.Array, data.Offset, data.Count);
        }
    }
}