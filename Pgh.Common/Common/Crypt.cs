using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Pgh.Common.Common
{
    public static class Crypt
    {
        //symétrique  Or Asymétrique 
        private const String StrPermutation = "ouiveyxaqtd";
        private const Int32 BytePermutation1 = 0x19;
        private const Int32 BytePermutation2 = 0x59;
        private const Int32 BytePermutation3 = 0x17;
        private const Int32 BytePermutation4 = 0x41;

        public static string EncryptCode(string toEncode)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(toEncode)));
        }

        public static string DecrypteCode(string hashCode)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(hashCode)));
        }

        private static byte[] Encrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(StrPermutation,
            new byte[] { BytePermutation1,
                         BytePermutation2,
                         BytePermutation3,
                         BytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        // decrypt
        private static byte[] Decrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(StrPermutation,
            new byte[] { BytePermutation1,
                         BytePermutation2,
                         BytePermutation3,
                         BytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
    }
}