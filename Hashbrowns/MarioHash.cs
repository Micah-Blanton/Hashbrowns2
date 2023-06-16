using System;
using System.Security.Cryptography;
using System.Text;
using CryptSharp;
using HashLib;
using HashLib.Crypto;
using HashLib.Hash32;
using HashLib.Hash64;

namespace MarioHash
{
    public static class MarioHash
    {
        public static string Hash_SHA1(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);
                return ConvertToHexString(hashBytes);
            }
        }

        public static string Hash_SHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return ConvertToHexString(hashBytes);
            }
        }

        public static string Hash_SHA2(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha512.ComputeHash(inputBytes);
                return ConvertToHexString(hashBytes);
            }
        }

        public static string Hash_SHA3(string input)
        {
            return "Coming Soon!";
        }
        public static string Hash_MD4(string input)
        {
            IHash md4 = HashFactory.Crypto.CreateMD4();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md4.ComputeBytes(inputBytes).GetBytes();
            return ConvertToHexString(hashBytes);
        }

        public static string Hash_MD5(string input)
        {
            return Crypter.MD5.Crypt(input);
        }

        public static string Hash_BCRYPT(string input)
        {
            return Crypter.Blowfish.Crypt(input);
        }

        public static string Hash_Whirlpool(string input)
        {
            IHash whirlpool = HashFactory.Crypto.CreateWhirlpool();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = whirlpool.ComputeBytes(inputBytes).GetBytes();
            return ConvertToHexString(hashBytes);
        }

        public static string Hash_PBKDF2(string input)
        {
            byte[] salt = new byte[16];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(salt);
            }

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, 10000))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // Change the output size if needed
                return ConvertToHexString(hashBytes);
            }
        }

        private static string ConvertToHexString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2")); // Convert byte to hexadecimal string
            }
            return builder.ToString();
        }
    }
}
