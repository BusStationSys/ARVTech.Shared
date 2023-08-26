namespace ARVTech.Shared
{
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Class responsible for functions and methods related to password and cryptography.
    /// RB: https://www.c-sharpcorner.com/article/encryption-and-decryption-using-a-symmetric-key-in-c-sharp/.
    /// </summary>
    public static class PasswordCryptography
    {
        /// <summary>
        /// Static method that use a symmetric key for decryption.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="encryptedValue"></param>
        /// <returns>Decrypted value.</returns>
        public static string DecryptString(string key, string encryptedValue)
        {
            try
            {
                byte[] iv = new byte[16];   //  Initialization Vector.

                byte[] buffer = Convert.FromBase64String(
                    encryptedValue);

                using (var aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(
                        key);

                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (var memoryStream = new MemoryStream(
                        buffer))
                    {
                        using (var cryptoStream = new CryptoStream(
                            (Stream)memoryStream,
                            decryptor,
                            CryptoStreamMode.Read))
                        {
                            using (var streamReader = new StreamReader(
                                (Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Static method that use a symmetric key for encryption.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="normalValue"></param>
        /// <returns>Encrypted value.</returns>
        public static string EncryptString(string key, string normalValue)
        {
            byte[] iv = new byte[16];   //  Initialization Vector.

            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(
                    key);

                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(
                    aes.Key,
                    aes.IV);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(
                        (Stream)memoryStream,
                        encryptor,
                        CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(
                            (Stream)cryptoStream))
                        {
                            streamWriter.Write(
                                normalValue);
                        }

                        return Convert.ToBase64String(
                            memoryStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Get hexadecimal string for MD5.
        /// </summary>
        /// <param name="inputValue">String to be passed.</param>
        /// <returns>Content with the hexadecimal string.</returns>
        public static string GetHashMD5(string inputValue)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the valor string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(inputValue));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sbBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sbBuilder.Append(data[i].ToString(
                        "x2",
                        CultureInfo.InvariantCulture));
                }

                // Return the hexadecimal string.
                return sbBuilder.ToString();
            }
        }
    }
}