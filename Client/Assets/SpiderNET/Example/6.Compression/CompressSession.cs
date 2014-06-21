using UnityEngine;
using System.Collections;
using SpiderNET.Core;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.Text;

namespace Example.Compression
{
    public class CompressSession : Session
    {
        protected static readonly string ACCEPT_ENCODING = "Accept-Encoding";
        protected static readonly string CONTENT_ENCODING = "Content-Encoding";
        protected static readonly string ENCODING_GZIP = "gzip";

        public override void Send(IMessage message)
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (GZipOutputStream zipStream = new GZipOutputStream(outputStream))
                {
                    zipStream.Write(message.RawData.Array, message.RawData.Offset, message.RawData.Count);
                }
                Headers[CONTENT_LENGTH] = outputStream.ToArray().Length.ToString();
                Headers[CONTENT_TYPE] = message.ContentType;
                Headers[ACCEPT_ENCODING] = ENCODING_GZIP;
                StartCoroutine(ReceiveCallback(new WWW(string.Format(URL, RemoteAddress, Port, message.ID), outputStream.ToArray(), Headers), message.ID));

                OnSend(message.ID);
            }
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
                if (string.IsNullOrEmpty(contentEncoding) == false && www.responseHeaders[contentEncoding].Equals(ENCODING_GZIP, System.StringComparison.OrdinalIgnoreCase) == true)
                {
                    using (MemoryStream inputStream = new MemoryStream())
                    {
                        using (GZipInputStream zipStream = new GZipInputStream(new MemoryStream(www.bytes)))
                        {
                            byte[] buffer = new byte[4096];
                            int count = 0;
                            do
                            {
                                count = zipStream.Read(buffer, 0, www.bytes.Length);
                                inputStream.Write(buffer, 0, count);
                            } while (count != 0);
                        }
                        byte[] receiveData = inputStream.ToArray();
                        OnReceive(id, receiveData, 0, receiveData.Length);
                    }
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