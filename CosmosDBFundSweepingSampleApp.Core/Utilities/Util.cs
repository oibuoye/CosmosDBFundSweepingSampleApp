using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.Utilities
{
    public class Util
    {

        public static string GenerateClientSecretKey()
        {
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[45];
            cryptoRandomDataGenerator.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public static string GenerateClientCode(string clientName)
        {
            byte[] preHash = Encoding.UTF32.GetBytes(clientName);
            byte[] hash = null;
            using (SHA256 sha = SHA256.Create())
            {
                hash = sha.ComputeHash(preHash);
            }
            return Convert.ToBase64String(hash);
        }

        public static string ComputeHash(string value, string secretKey)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(secretKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(value);

            byte[] hashmessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);

            // to lowercase hexits
            String.Concat(Array.ConvertAll(hashmessage, x => x.ToString("x2")));

            // to base64
            return Convert.ToBase64String(hashmessage);
        }

        public static string ZeroPadUp(string value, int maxPadding, string prefix = null)
        {
            string result = value.PadLeft(maxPadding, '0');
            if (!string.IsNullOrEmpty(prefix)) { return prefix + result; }
            return result;
        }

    }
}
