namespace ARVTech.Shared.Email
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailData
    {
        /// <summary>
        /// Default value <c>False</c> and so the content will be in plain text. <c>True</c> for content in HTML format.
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Recipient`s e-mail address.
        /// </summary>
        public string ReceiverEmail { get; set; }

        /// <summary>
        /// Recipient`s name.
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// E-mail subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// E-mail body.
        /// </summary>
        public string Body { get; set; }
    }
}