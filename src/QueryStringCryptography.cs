namespace ARVTech.Shared
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Class responsible for Querystring encryption.
    /// </summary>
    public static class QueryStringCryptography
    {
        private static readonly byte[] _iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// Encrypt the given byte data using the default <c>DESCryptoServiceProvider</c>.
        /// </summary>
        /// <param name="stringToEncrypt">Value to be encrypted.</param>
        /// <param name="sEncryptionKey">Encryption`s key.</param>
        /// <returns>Encrypted data.</returns>
        public static string Encrypt(string stringToEncrypt, string sEncryptionKey)
        {
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Decrypt the given byte data using the default <c>DESCryptoServiceProvider</c>.
        /// </summary>
        /// <param name="stringToDecrypt">Value to be decrypted.</param>
        /// <param name="sEncryptionKey">Encryption`s key.</param>
        /// <returns>Decrypted data.</returns>
        public static string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            byte[] inputByteArray = new byte[stringToDecrypt.Length];

            try
            {
                byte[] key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8;

                return encoding.GetString(ms.ToArray());
            }
            catch
            {
                throw;
            }
        }
    }
}
