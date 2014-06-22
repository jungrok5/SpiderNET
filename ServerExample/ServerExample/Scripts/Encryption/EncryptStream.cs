using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ServerExample.Scripts.Encryption
{
    public class EncryptStream : Stream
    {
        public EncryptStream(Stream stream, string encryptKey)
        {
            BaseStream = stream;
            EncryptKey = encryptKey;
            ms = new MemoryStream();
        }

        private byte[] buffer = new byte[8192];
        public Stream BaseStream { get; set; }
        public string EncryptKey { get; set; }
        private MemoryStream ms;

        public override bool CanRead { get { return true; } }
        public override bool CanSeek { get { return false; } }
        public override bool CanWrite { get { return false; } }
        public override long Length { get { return BaseStream.Length; } }
        public override long Position { get { return BaseStream.Position; } set { BaseStream.Position = value; } }
        //public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
        //{
        //    return BaseStream.BeginRead(array, offset, count, asyncCallback, asyncState);
        //}
        //public override IAsyncResult BeginWrite(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
        //{
        //    return BaseStream.BeginWrite(array, offset, count, asyncCallback, asyncState);
        //}

        //public override int EndRead(IAsyncResult asyncResult)
        //{
        //    return BaseStream.EndRead(asyncResult);
        //}
        //public override void EndWrite(IAsyncResult asyncResult)
        //{
        //    BaseStream.EndWrite(asyncResult);
        //}
        public override void Flush()
        {
            BaseStream.Flush();
        }
        public override int Read(byte[] array, int offset, int count)
        {
            // TODO
            int readBytes = BaseStream.Read(buffer, 0, buffer.Length);
            if (readBytes > 0)
            {
                byte[] decryptData = AES.Decrypt(buffer, 0, readBytes, EncryptKey);
                Array.Copy(decryptData, 0, array, offset, decryptData.Length);
            }
            return readBytes;
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }
        public override void SetLength(long value)
        {
            BaseStream.SetLength(value);
        }
        public override void Write(byte[] array, int offset, int count)
        {
            byte[] encryptData = AES.Encrypt(array, offset, count, EncryptKey);
            BaseStream.Write(encryptData, 0, encryptData.Length);
        }
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing == true)
                {
                    if (BaseStream != null)
                        BaseStream.Close();
                    if (ms != null)
                        ms.Close();
                }
                BaseStream = null;
                ms = null;
                buffer = null;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
    }
}