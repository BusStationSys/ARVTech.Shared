namespace ARVTech.Shared.Email
{
    public interface IEmailService
    {
        void SendMail(EmailData emailData);
    }
}