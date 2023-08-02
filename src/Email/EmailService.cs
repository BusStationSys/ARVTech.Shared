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
        ///// <summary>
        ///// Holds the instance of the <see cref="SmtpDeliveryMethod"/> enum.
        ///// </summary>
        //private readonly SmtpDeliveryMethod _smtpDeliveryMethod;

        ///// <summary>
        ///// Holds the name or IP address of the Host used.
        ///// </summary>
        //private readonly string _host;

        ///// <summary>
        ///// Holds the number of the Port used.
        ///// </summary>
        //private readonly int _port;

        ///// <summary>
        ///// Holds whether the uses Secure Socket Layer to encrypt connection.
        ///// </summary>
        //private readonly bool _enableSsl;

        ///// <summary>
        ///// Holds the username associated with the credentials and sender of the email.
        ///// </summary>
        //private readonly string _email = string.Empty;

        ///// <summary>
        ///// Holds the password associated with the credentials.
        ///// </summary>
        //private readonly string _password = string.Empty;

        ///// <summary>
        ///// Holds the display name that will be displayed on the email`s sender.
        ///// </summary>
        //private readonly string _displayName;

        //        "Server": "smtp.arvtech.com.br",
        //"Port": 587,
        //"SenderName": "PayCheck",
        //"SenderEmail": "noreply@arvtech.com.br",
        //"Username": "noreply@arvtech.com.br",
        //"Password": "(n0r391y)"

        private readonly string _server;

        private readonly int _port;

        private readonly string _senderName;

        private readonly string _senderEmail;

        private readonly string _username;

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

        ///// <summary>
        ///// Initializes a new instance of the <see cref="Email"/> class.
        ///// </summary>
        ///// <param name="smtpDeliveryMethod">Enum that indicates how the message will be delivered.</param>
        ///// <param name="host">Name or IP address of the Host used.</param>
        ///// <param name="port">Number of the Port used.</param>
        ///// <param name="enableSsl">Indicates the uses Secure Socket Layer to encrypt connection.</param>
        ///// <param name="email">Username associated with the credentials and sender of the email.</param>
        ///// <param name="password">Password associated with the credentials.</param>
        //public EmailService(SmtpDeliveryMethod smtpDeliveryMethod, string host, int port, bool enableSsl, string email, string password, string displayName = ".:ARVTech:.")
        //{
        //    _smtpDeliveryMethod = smtpDeliveryMethod;
        //    _host = host;
        //    _port = port;
        //    _enableSsl = enableSsl;
        //    _email = email;
        //    _password = password;

        //    this._displayName = displayName;
        //}

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

        //public void Enviar(string emailDestinatario, string assuntoDestinatario, string content, bool isContentHTML = false)
        //{
        //    try
        //    {
        //        NetworkCredential credentials = new NetworkCredential(
        //            _email,
        //            _password);

        //        using (var smtpClient = new SmtpClient()
        //        {
        //            Host = _host,
        //            Port = _port,
        //            UseDefaultCredentials = false,   // gmail
        //            Credentials = credentials,
        //            EnableSsl = _enableSsl,
        //            DeliveryMethod = _smtpDeliveryMethod,
        //        })
        //        {
        //            // Setting`s "From, To and CC".
        //            using (MailMessage mail = new MailMessage()
        //            {
        //                From = new MailAddress(
        //                    _email,
        //                    _displayName,
        //                    Encoding.UTF8),
        //                Body = content,
        //                IsBodyHtml = isContentHTML,
        //                Subject = assuntoDestinatario,
        //            })
        //            {
        //                mail.To.Add(new MailAddress(_email));
        //                mail.CC.Add(new MailAddress(emailDestinatario));

        //                smtpClient.Send(mail);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public void SendMail(EmailData emailData)
        //{
        //    try
        //    {
        //        using (MimeMessage emailMessage = new())
        //        {
        //            MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
        //            emailMessage.From.Add(emailFrom);
        //            MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
        //            emailMessage.To.Add(emailTo);

        //            emailMessage.Cc.Add(new MailboxAddress("Cc Receiver", "cc@example.com"));
        //            emailMessage.Bcc.Add(new MailboxAddress("Bcc Receiver", "bcc@example.com"));

        //            emailMessage.Subject = mailData.EmailSubject;

        //            BodyBuilder emailBodyBuilder = new BodyBuilder();
        //            emailBodyBuilder.TextBody = mailData.EmailBody;

        //            emailMessage.Body = emailBodyBuilder.ToMessageBody();

        //            //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
        //            using (SmtpClient mailClient = new SmtpClient())
        //            {
        //                mailClient.Connect(this._server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        //                mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
        //                mailClient.Send(emailMessage);
        //                mailClient.Disconnect(true);
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Exception Details
        //        return false;
        //    }
        //}
    }
}