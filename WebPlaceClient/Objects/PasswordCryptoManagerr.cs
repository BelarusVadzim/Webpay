using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebPlace.Objects
{
    public class PasswordCryptoManager
    {
        public bool CheckPassword(string Password)
        {
            return (WebPlaceSettings.PasswordHash == GetHashString(Password));
        }

        public void SetNewPassword(string Password)
        {
            WebPlaceSettings.PasswordHash = GetHashString(Password);
            WebPlaceSettings.Save();
        }

        private byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
