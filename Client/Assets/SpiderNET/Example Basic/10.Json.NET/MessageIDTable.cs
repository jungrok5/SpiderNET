using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Example.JsonNET
{
    public class MessageIDTable
    {
        private Dictionary<MessageID, string> Table = new Dictionary<MessageID, string>();

        public void AddID(MessageID id, string url)
        {
            if (Table.ContainsKey(id) == true)
                return;
            Table.Add(id, url);
        }

        public string this[MessageID id]
        {
            get
            {
                if (Table.ContainsKey(id) == false)
                    return string.Empty;
                return Table[id];
            }
        }

        public MessageID this[string url]
        {
            get
            {
                foreach (var kvp in Table)
                {
                    if (kvp.Value == url)
                        return kvp.Key;
                }
                return MessageID.Unknown;
            }
        }

        public MessageID[] IDs
        {
            get
            {
                return Table.Keys.ToArray();
            }
        }
    }
}