using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MakeNMake.Utilities
{
    public static class EncryptDecrypt
    {
        #region Security
        public static string Encript(string inputvalue)
        {
           // string EncryptionKey = "6FF1D65C0D2B";
            string strmsg = string.Empty;
            byte[] encode = new byte[inputvalue.Length];
            encode = Encoding.UTF8.GetBytes(inputvalue);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public static string DecryptText(string cipherText)
        {
          //  string EncryptionKey = Convert.ToString("6FF1D65C0D2B");
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(cipherText);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        #endregion

        #region RandomNumber
        public static string CreateRandomPassword(int PasswordLength)
        {
           // string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            string _allowedChars = "0123456789";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        #endregion
    }
}
