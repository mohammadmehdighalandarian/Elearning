using System.Net;
using System.Net.Mail;

namespace LearningWeb_Core.Senders
{
    public class SendEmail
    {

        public static void Send(string to, string subject, string body)
        {
            var fromAddress = new MailAddress("mohammadmehdighn@gmail.com", "Admin");
            var toAddress = new MailAddress(to, "To Name");
            const string fromPassword = "vycluhccxqthdqju";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
