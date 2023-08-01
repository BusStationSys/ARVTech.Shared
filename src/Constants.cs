namespace ARVTech.Shared
{
    /// <summary>
    /// Class to hold the common constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Holds the key used in encrypt`s methods.
        /// </summary>
        public const string EncryptionKey = "(@rV73Ch)";

        /// <summary>
        /// Holds the pattern used in email`s Regex.
        /// </summary>
        public const string PatternRegexEmail = @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$";
    }
}