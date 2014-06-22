using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using System.IO;
using System.Text;

namespace Example.Encryption
{
    public class EncryptSession : Session
    {
        protected static readonly string ACCEPT_ENCODING = "Accept-Encoding";
        protected static readonly string CONTENT_ENCODING = "Content-Encoding";
        protected static readonly string ENCODING_ENCRYPT = "encrypt";

        public string EncryptKey { get; set; }

        public override void Send(IMessage message)
        {
            if (string.IsNullOrEmpty(EncryptKey) == false)
            {
                byte[] encryptedData = AES.Encrypt(message.RawData.Array, message.RawData.Offset, message.RawData.Count, EncryptKey);
                Headers[CONTENT_LENGTH] = encryptedData.Length.ToString();
                Headers[CONTENT_TYPE] = message.ContentType;
                Headers[ACCEPT_ENCODING] = ENCODING_ENCRYPT;
                StartCoroutine(ReceiveCallback(new WWW(string.Format(URL, RemoteAddress, Port, message.ID), encryptedData, Headers), message.ID));
            }
            else
            {
                Headers[CONTENT_LENGTH] = message.RawData.Count.ToString();
                Headers[CONTENT_TYPE] = message.ContentType;
                StartCoroutine(ReceiveCallback(new WWW(string.Format(URL, RemoteAddress, Port, message.ID), message.RawData.Array, Headers), message.ID));
            }

            OnSend(message.ID);
        }

        protected override IEnumerator ReceiveCallback(WWW www, string id)
        {
            yield return www;
            if (www.error == null)
            {
                string contentEncoding = string.Empty;
                foreach (var kvp in www.responseHeaders)
                {
                    if (kvp.Key.Equals(CONTENT_ENCODING, System.StringComparison.OrdinalIgnoreCase) == true)
                    {
                        contentEncoding = kvp.Key;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(EncryptKey) == false && 
                    string.IsNullOrEmpty(contentEncoding) == false && 
                    www.responseHeaders[contentEncoding].Equals(ENCODING_ENCRYPT, System.StringComparison.OrdinalIgnoreCase) == true)
                {
                    byte[] decryptedData = AES.Decrypt(www.bytes, 0, www.bytes.Length, EncryptKey);
                    OnReceive(id, decryptedData, 0, decryptedData.Length);
                }
                else
                {
                    OnReceive(id, www.bytes, 0, www.bytes.Length);
                }
            }
            else
                OnError(id, www.error, null);
            www.Dispose();
        }
    }
}