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

        public static string GetDecrypt(string hookSecret, string message)
            => DecryptStringFromBytes_Aes(HEX2Bytes(message), Encoding.Default.GetBytes(hookSecret.Replace(" ", "")));

        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");

            Key = NormaKey(Key);

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key.
            using Aes aesAlg = Aes.Create();
            aesAlg.Mode = CipherMode.ECB;
            aesAlg.Key = Key;
            aesAlg.Padding = PaddingMode.Zeros;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor();

            // Create the streams used for decryption.
            using MemoryStream msDecrypt = new MemoryStream(cipherText);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);

            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.
            plaintext = srDecrypt.ReadToEnd();

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

        private static byte[] NormaKey(byte[] key)
        {
            byte[] norma = new byte[KEY_COUNT];

            for (int i = 0; i < KEY_COUNT; i++)
                norma[i] = (byte)i;

            if (key.Length < KEY_COUNT)
                for (int i = 0; i < key.Length; i++)
                    norma[i] = key[i];

            return norma;
        }
    }
}