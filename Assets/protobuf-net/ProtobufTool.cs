using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ProtoBuf;
using UnityEngine;

namespace Bufan.Proto
{
    public class ProtobufTool
    {

        public static byte[] ProtoBufToBytes<T>(T t)
        {
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize<T>(memoryStream, t);
                result = memoryStream.ToArray();
            }
            return EncryptDES(result, keyss);
        }

        public static T BytesToProtoBuf<T>(byte[] bytes, int offset = 0, int size =-1)
        {
            T result;
            bytes = DecryptDES(bytes, keyss);
            size = size == -1 ? bytes.Length : size;
            using (MemoryStream memoryStream = new MemoryStream(bytes, offset, size))
            {
                result = Serializer.Deserialize<T>(memoryStream);
            }
            return result;
        }

        //密钥  
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        public static string keyss = "bfbf2333";
        public static byte[] EncryptDES(byte []bytes, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = bytes;
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return mStream.ToArray();
            }
            catch
            {
                return bytes;
            }
        }

        public static string EncryptDESByStr(string encryptString, string encryptKey)
        {
            return Convert.ToBase64String(EncryptDES(Encoding.UTF8.GetBytes(encryptString), encryptKey));
        }

        public static byte[] DecryptDES(byte[] bytes, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = bytes;
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return mStream.ToArray();
            }
            catch
            {
                return bytes;
            }
        }

        public static string DecryptDESByStr(string decryptString, string decryptKey)
        {
            return Convert.ToBase64String(DecryptDES(Encoding.UTF8.GetBytes(decryptString), decryptKey));
        }

    }

}
