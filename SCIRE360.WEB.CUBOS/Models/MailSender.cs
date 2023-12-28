using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace SCIREValidador.Models
{
    public class MailSender
    {
        public async Task<bool> sendMail(string toEmailAddress, string subject, string htmlMessage)
        {
            SmtpClient SmtpServer = new SmtpClient("mail.alvisoft.net.");
            var mail = new MailMessage();
            mail.From = new MailAddress("icarreno@alvisoft.net");
            mail.To.Add(toEmailAddress);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = htmlMessage;
            SmtpServer.Port = 25;
            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Credentials = new System.Net.NetworkCredential("icarreno@alvisoft.net", "Alvis0ftirving");
            SmtpServer.EnableSsl = false;

            try
            {
                await Task.Run(() => SmtpServer.Send(mail));
                return true;
            }
            catch (Exception) {return false;}
        }
    }
}