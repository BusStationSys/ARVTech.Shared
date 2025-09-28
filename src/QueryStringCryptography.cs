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
        /// <param name="keyEncrypt">Encryption`s key.</param>
        /// <returns>Encrypted data.</returns>
        public static string Encrypt(string stringToEncrypt, string keyEncrypt)
        {
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(keyEncrypt[..8]);

                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);

                using (var des = new DESCryptoServiceProvider())
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, des.CreateEncryptor(key, _iv), CryptoStreamMode.Write))
                        {
                            cs.Write(
                                inputByteArray,
                                0, 
                                inputByteArray.Length);

                            cs.FlushFinalBlock();
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
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
        /// <param name="keyEncrypt">Encryption`s key.</param>
        /// <returns>Decrypted data.</returns>
        public static string Decrypt(string stringToDecrypt, string keyEncrypt)
        {
            try
            {
                byte[] inputByteArray = new byte[stringToDecrypt.Length];

                byte[] key = Encoding.UTF8.GetBytes(keyEncrypt[..8]);

                using (var des = new DESCryptoServiceProvider())
                {
                    inputByteArray = Convert.FromBase64String(stringToDecrypt.Replace(" ", "+"));

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, des.CreateDecryptor(key, _iv), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                        }

                        return Encoding.UTF8.GetString(
                            ms.ToArray());
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
