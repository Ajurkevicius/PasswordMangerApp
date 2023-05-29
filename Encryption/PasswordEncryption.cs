using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageRecognition.Encryption
{
    public static class PasswordEncryption
    {
        private static readonly byte[] Salt = Encoding.UTF8.GetBytes("MySalt"); // Use a random salt value
        private const int Iterations = 10000; // Use a high number of iterations to make key derivation more expensive
        private const int KeySize = 256; // Use a 256-bit key size for AES

        public static string Encrypt(string plaintext, string masterPassword)
        {
            using var aes = Aes.Create();
            var key = DeriveKeyFromMasterPassword(masterPassword, aes.KeySize / 8);
            aes.Key = key;
            aes.IV = GenerateRandomIV();
            using var encryptor = aes.CreateEncryptor();
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            cryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var ciphertextBytes = memoryStream.ToArray();
            var ivAndCiphertext = new byte[aes.IV.Length + ciphertextBytes.Length];
            Buffer.BlockCopy(aes.IV, 0, ivAndCiphertext, 0, aes.IV.Length);
            Buffer.BlockCopy(ciphertextBytes, 0, ivAndCiphertext, aes.IV.Length, ciphertextBytes.Length);
            return Convert.ToBase64String(ivAndCiphertext);
        }

        public static string Decrypt(string ciphertext, string masterPassword)
        {
            try
            {

                using var aes = Aes.Create();
                var key = DeriveKeyFromMasterPassword(masterPassword, aes.KeySize / 8);
                aes.Key = key;
                var ivAndCiphertext = Convert.FromBase64String(ciphertext);
                var iv = new byte[aes.IV.Length];
                var ciphertextBytes = new byte[ivAndCiphertext.Length - aes.IV.Length];
                Buffer.BlockCopy(ivAndCiphertext, 0, iv, 0, aes.IV.Length);
                Buffer.BlockCopy(ivAndCiphertext, aes.IV.Length, ciphertextBytes, 0, ciphertextBytes.Length);
                aes.IV = iv;
                using var decryptor = aes.CreateDecryptor();
                using var memoryStream = new MemoryStream(ciphertextBytes);
                using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using var reader = new StreamReader(cryptoStream);
                return reader.ReadToEnd();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Wrong credentials");
                return "";
            }
           
        }

        private static byte[] DeriveKeyFromMasterPassword(string masterPassword, int keySize)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(masterPassword, Salt, Iterations);
            return pbkdf2.GetBytes(keySize);
        }

        private static byte[] GenerateRandomIV()
        {
            using var rng = new RNGCryptoServiceProvider();
            var iv = new byte[16]; // Use a 16-byte IV for AES
            rng.GetBytes(iv);
            return iv;
        }
    }
}
