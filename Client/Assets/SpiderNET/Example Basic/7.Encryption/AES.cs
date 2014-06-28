using UnityEngine;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System;

namespace Example.Encryption
{
    public static class AES
    {
        private static ICryptoTransform encryptTransform;
        private static ICryptoTransform decryptTransform;

        public static void Init(string key)
        {
            byte[] keyData = UTF8Encoding.UTF8.GetBytes(key);

            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.Key = keyData;
            rijndael.Mode = CipherMode.ECB;
            rijndael.Padding = PaddingMode.PKCS7;

            encryptTransform = rijndael.CreateEncryptor();
            decryptTransform = rijndael.CreateDecryptor();
        }

        public static byte[] Encrypt(byte[] data, int offset, int length)
        {
            if (encryptTransform == null)
                return null;

            try
            {
                return encryptTransform.TransformFinalBlock(data, offset, length);
            }
            catch
            {
                return null;
            }
        }

        public static byte[] Decrypt(byte[] data, int offset, int length)
        {
            if (decryptTransform == null)
                return null;

            try
            {
                return decryptTransform.TransformFinalBlock(data, offset, length);
            }
            catch
            {
                return null;
            }
        }
    }
}