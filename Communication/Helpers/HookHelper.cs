using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Filuet.Infrastructure.Communication.Helpers
{
    public static class HookHelpers
    {
        private const int KEY_COUNT = 32;

        public static string Decrypt(string hookSecret, string message)
            => DecryptStringFromBytes_Aes(HEX2Bytes(message), Encoding.Default.GetBytes(hookSecret.Replace(" ", "")));

        public static string Encrypt(string hookSecret, string serializedBody)
        {
            byte[] key = Encoding.Default.GetBytes(hookSecret.Replace(" ", ""));
            byte[] bytes = EncryptStringToBytes_Aes(serializedBody, key);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key)
        {
            // Check arguments
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");

            Key = NormKey(Key);

            // Declare the string used to hold the decrypted text
            string plaintext = null;

            // Create an Aes object with the specified key
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Key = Key;
                aesAlg.Padding = PaddingMode.Zeros;

                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = aesAlg.CreateDecryptor();

                // Create the streams used for decryption
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream and place them in a string
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        private static byte[] HEX2Bytes(string hex)
        {
            if (hex.Length % 2 != 0)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hex));

            byte[] HexAsBytes = new byte[hex.Length / 2];
            for (int index = 0; index < HexAsBytes.Length; index++)
            {
                string byteValue = hex.Substring(index * 2, 2);
                HexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return HexAsBytes;
        }

        private static byte[] EncryptStringToBytes_Aes(string request, byte[] key)
        {
            if (request == null || request.Length <= 0)
                throw new ArgumentNullException("plainText");

            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");

            key = NormKey(key);

            byte[] encrypted;
            // Create an Aes object with the specified key and IV
            using (Aes aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Key = key;
                aesAlg.Padding = PaddingMode.Zeros;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor();

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(request);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }

                aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream
            return encrypted;
        }

        private static byte[] NormKey(byte[] key)
        {
            byte[] norm = new byte[KEY_COUNT];

            for (int i = 0; i < KEY_COUNT; i++)
                norm[i] = (byte)i;

            if (key.Length < KEY_COUNT)
                for (int i = 0; i < key.Length; i++)
                    norm[i] = key[i];

            return norm;
        }
    }
}