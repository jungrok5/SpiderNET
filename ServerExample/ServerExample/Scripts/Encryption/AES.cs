using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ServerExample.Scripts.Encryption
{
    public static class AES
    {
        public static byte[] Encrypt(byte[] data, int offset, int length, string key)
        {
            try
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
                //byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();

                byte[] resultData = cTransform.TransformFinalBlock(data, offset, length);
                //return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                return resultData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static byte[] Decrypt(byte[] data, int offset, int length, string key)
        {
            try
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
                //toDecrypt = HttpUtility.UrlDecode(toDecrypt);
                //byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();

                byte[] resultData = cTransform.TransformFinalBlock(data, offset, length);
                //return UTF8Encoding.UTF8.GetString(resultArray);
                return resultData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}