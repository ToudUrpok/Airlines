using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Common
{
    public static class Crypto
    {
        public static string Sha256(string value)
        {
            var sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                foreach (var b in hash.ComputeHash(Encoding.UTF8.GetBytes(value)))
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
