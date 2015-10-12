using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LimeSystems
{
    public class CryptoProvider
    {
        
        public static string GetMD5Hash(string p)
        {
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] hashCode = md5Provider.ComputeHash(Encoding.Default.GetBytes(p));
            return BitConverter.ToString(hashCode).ToLower().Replace("-", "");
        }
    }
}