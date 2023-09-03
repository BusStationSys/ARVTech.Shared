namespace ARVTech.Shared.Email
{
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// Class responsible for sending e-mail.
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Holds the name or IP address or Server of the Host used.
        /// </summary>
        private readonly string _server;

        /// <summary>
        /// Holds the number of the Port used.
        /// </summary>
        private readonly int _port;

        /// <summary>
        /// Holds the info that will be displayed on the name`s sender.
        /// </summary>
        private readonly string _senderName;

        /// <summary>
        /// Holds the info that will be displayed on the email`s sender.
        /// </summary>
        private readonly string _senderEmail;

        /// <summary>
        /// Holds the username associated with the credentials.
        /// </summary>
        private readonly string _username;

        /// <summary>
        /// Holds the password associated with the credentials.
        /// </summary>
        private readonly string _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="senderName"></param>
        /// <param name="senderEmail"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public EmailService(string server, int port, string senderName, string senderEmail, string username, string password)
        {
            this._server = server;
            this._port = port;
            this._senderName = senderName;
            this._senderEmail = senderEmail;
            this._username = username;
            this._password = password;
        }

        /// <summary>
        /// Method responsible for sending the e-mail.
        /// </summary>
        /// <param name="emailData"></param>
        public void SendMail(EmailData emailData)
        {
            try
            {
                var credentials = new NetworkCredential
                {
                    UserName = this._username,
                    Password = this._password,
                };

                using (var smtpClient = new SmtpClient()
                {
                    Host = this._server,
                    Port = this._port,
                    UseDefaultCredentials = false,   // gmail
                    Credentials = credentials,
                    //EnableSsl = _enableSsl,
                    //DeliveryMethod = _smtpDeliveryMethod,
                })
                {
                    // Setting`s "From, To and CC".
                    using (MailMessage mail = new MailMessage()
                    {
                        From = new MailAddress(
                            this._senderEmail,
                            this._senderName,
                            Encoding.UTF8),
                        Body = emailData.Body,
                        IsBodyHtml = emailData.IsBodyHtml,
                        Subject = emailData.Subject,
                    })
                    {
                        mail.To.Add(new MailAddress(this._username));
                        mail.CC.Add(new MailAddress(emailData.ReceiverEmail));

                        smtpClient.Send(mail);
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