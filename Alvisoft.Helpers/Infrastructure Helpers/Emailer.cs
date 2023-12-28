using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using Alvisoft.Helpers;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Alvisoft.Helpers
{

    /// <summary>
    /// Utility class for sending email
    /// Design and Architecture: Anatoly A. Pedemonte Ku
    /// </summary>
    public sealed class Emailer
    {
        private Emailer()
        {
        }

        public static void SendMail(string subject, string to,
            string from = null, string body = null, Stream attachment = null,
            int port = 25, string host = "localhost", bool isBodyHtml = true)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(from);
            mailMsg.To.Add(to);
            mailMsg.Subject = subject;
            mailMsg.IsBodyHtml = isBodyHtml;
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.Body = body;
            mailMsg.Priority = MailPriority.Normal;

            //Message attahment
            if (attachment != null)
                mailMsg.Attachments.Add(new Attachment(attachment, "my.text"));

            // Smtp configuration
            SmtpClient client = new SmtpClient();
            //client.Credentials = new NetworkCredential("test", "test");
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Port = port; //use 465 or 587 for gmail           
            client.Host = host;//for gmail "smtp.gmail.com";
            client.EnableSsl = false;

            MailMessage message = mailMsg;

            client.Send(message);

        }

        //public static bool Mailer(string sSubject, string sBody, string sToName, string sToEmail, bool sBodyHTML, bool sAttach, string FileAttach, int Type)
        //{

        //    try
        //    {
        //        MailAddress objFrom = new MailAddress(ConfigurationManager.AppSettings.Get("FromEmail").ToString(), ConfigurationManager.AppSettings.Get("FromName").ToString());
        //        MailAddress objto = new MailAddress(sToEmail, sToName);

        //        MailMessage msgMail = new MailMessage(objFrom, objto);

        //        // Dim objCC As MailAddress = New MailAddress(ConfigurationManager.AppSettings("CCEmail").ToString)

        //         // msgMail.CC.Add(objCC)

        //        MailAddress objBCC = new MailAddress(ConfigurationManager.AppSettings.Get("BCCEmail").ToString());

        //        msgMail.Bcc.Add(objBCC);
        //        msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        //        msgMail.Priority = MailPriority.High;

        //        msgMail.IsBodyHtml = sBodyHTML;
        //        msgMail.Subject = ConfigurationManager.AppSettings.Get("Subject") + " " + sSubject;
        //        msgMail.Body = sBody;

        //        if (sAttach == true)
        //        {
        //            System.Net.Mail.Attachment attachment;
        //            attachment = new System.Net.Mail.Attachment(FileAttach);
        //            msgMail.Attachments.Add(attachment);
        //        }

        //        System.Net.Mail.SmtpClient objSMTP = new System.Net.Mail.SmtpClient();

        //        objSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;

        //        //if (ConfigurationManager.AppSettings.Get("MailServer").ToString() == "127.0.0.1")
        //        //{


        //        //    //objSMTP.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings.Get("UserName").ToString(), ConfigurationManager.AppSettings.Get("Password").ToString());
        //        //    objSMTP.Host = ConfigurationManager.AppSettings.Get("MailServer").ToString();
        //        //}
        //        //else
        //        //{

        //        //    objSMTP.UseDefaultCredentials = false;
        //        //    //  objSMTP.Credentials = strCredenciales



        //        if (Type == 1)
        //        {

        //            objSMTP.UseDefaultCredentials = true;
        //            objSMTP.Host = "localhost";

        //        }
        //        else
        //        {
        //            objSMTP.UseDefaultCredentials = false;
        //            objSMTP.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings.Get("UserName").ToString(), ConfigurationManager.AppSettings.Get("Password").ToString());
        //            objSMTP.Host = ConfigurationManager.AppSettings.Get("MailServer").ToString();
        //        }
        //        //}

        //        objSMTP.Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Port"));
        //        objSMTP.EnableSsl = false;
        //        objSMTP.Send(msgMail);

        //        return true;

        //    }
        //    catch (Exception ex)
        //    {

        //      ExceptionManager.DoLogAndGetFriendlyMessageForException(ex);
        //      return false;
        //    }

        //}

        public static bool Mailer(string sSubject, string sBody, string sToName, string sToEmail, bool sBodyHTML, bool sAttach, string FileAttach, int Type, DataTable tabDataMail,  out string Error)
        {
            Error = "";
            try
            {

                DataRow dr = tabDataMail.Rows[0];
                string UserName = dr["UserName"].ToString();
                string Password = dr["Password"].ToString();
                string FromEmail = dr["FromEmail"].ToString();
                string FromName = dr["FromName"].ToString();
                string BCCEmail = dr["BCCEmail"].ToString();
                string Subject = dr["Subject"].ToString();
                string MailServer = dr["MailServer"].ToString();
                string Port = dr["Port"].ToString();
                int Exchange = Convert.ToInt32(dr["Exchange"].ToString());
                bool bValCertificado = Convert.ToBoolean(dr["bValCertificado"].ToString());

                MailAddress objFrom = new MailAddress(FromEmail, FromName);
                MailAddress objto = new MailAddress(sToEmail, sToName);
                MailMessage msgMail = new MailMessage(objFrom, objto);
                MailAddress objBCC = new MailAddress(BCCEmail);

                msgMail.Bcc.Add(objBCC);
                msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure |
                                                      DeliveryNotificationOptions.OnSuccess |
                                                      DeliveryNotificationOptions.Delay;
                msgMail.Priority = MailPriority.High;

                msgMail.IsBodyHtml = sBodyHTML;
                msgMail.Subject = Subject + sSubject;
                msgMail.Body = sBody;

                if (sAttach == true)
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(FileAttach);
                    msgMail.Attachments.Add(attachment);
                }

                System.Net.Mail.SmtpClient objSMTP = new System.Net.Mail.SmtpClient();
                objSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;


                if (Type == 1)
                {

                    objSMTP.UseDefaultCredentials = true;
                    objSMTP.Host = "localhost";

                }
                else
                {
                    objSMTP.UseDefaultCredentials = false;
                    objSMTP.Credentials = new System.Net.NetworkCredential(
                        UserName,
                        Password);
                    objSMTP.Host = MailServer;
                }
                //}

                objSMTP.Port = Convert.ToInt32(Port);
                if (Exchange == 1)
                {
                    objSMTP.EnableSsl = true;
                }
                else
                {
                    objSMTP.EnableSsl = false;
                }
                if (Exchange == 1)
                {
                    objSMTP.TargetName = "STARTTLS/smtp.office365.com";
                }

                if (!bValCertificado)
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s
                        , X509Certificate certificate
                        , X509Chain chain
                        , SslPolicyErrors sslPolicyErrors)
                    { return true; };
                }

                objSMTP.Send(msgMail);
                objSMTP.Dispose();
                msgMail.Dispose();
                return true;

            }
            catch (Exception ex)
            {
                Error = "ERROR al completar la Operación :" + (ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                ExceptionManager.DoLogAndGetFriendlyMessageForException(ex);
                return false;
            }

        }

        public static bool Mailer(string sSubject, string sBody, string sToName, string sToEmail, bool sBodyHTML, bool sAttach, string FileAttach, int Type,int Exchange, out string Error)
        {
            Error = "";
            try
            {
                MailAddress objFrom = new MailAddress(ConfigurationManager.AppSettings.Get("FromEmail").ToString(), ConfigurationManager.AppSettings.Get("FromName").ToString());
                MailAddress objto = new MailAddress(sToEmail, sToName);
                MailMessage msgMail = new MailMessage(objFrom, objto);
                MailAddress objBCC = new MailAddress(ConfigurationManager.AppSettings.Get("BCCEmail").ToString());
                msgMail.Bcc.Add(objBCC);
                msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                msgMail.Priority = MailPriority.High;

                msgMail.IsBodyHtml = sBodyHTML;
                msgMail.Subject = ConfigurationManager.AppSettings.Get("Subject") + " " + sSubject;
                msgMail.Body = sBody;

                if (sAttach == true)
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(FileAttach);
                    msgMail.Attachments.Add(attachment);
                }

                System.Net.Mail.SmtpClient objSMTP = new System.Net.Mail.SmtpClient();
                objSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;

                if (Type == 1)
                {

                    objSMTP.UseDefaultCredentials = true;
                    objSMTP.Host = "localhost";

                }
                else
                {
                    objSMTP.Credentials = new System.Net.NetworkCredential(
                        ConfigurationManager.AppSettings.Get("UserName").ToString(),
                        ConfigurationManager.AppSettings.Get("Password").ToString());
                    objSMTP.UseDefaultCredentials = false;
                    objSMTP.Host = ConfigurationManager.AppSettings.Get("MailServer").ToString();
                }
                //}

                objSMTP.Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Port"));
                if (Exchange == 1)
                {
                    objSMTP.EnableSsl = true;
                }
                else {
                    objSMTP.EnableSsl = false;
                }
                if (Exchange == 1)
                {
                    objSMTP.TargetName = "STARTTLS/smtp.office365.com";
                }
                objSMTP.Send(msgMail);
                msgMail.Dispose();
                return true;

            }
            catch (Exception ex)
            {
                Error = "ERROR al completar la Operación :" + (ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                ExceptionManager.DoLogAndGetFriendlyMessageForException(ex);
                return false;
            }

        }



        public static bool MailerServer(string sSubject, string sBody, string sToName, string sToEmail, bool sBodyHTML, bool sAttach, string FileAttach, string server, string port, string user, string password, bool ssl, string email, string fromName)
        {

            try
            {
                MailAddress objFrom = new MailAddress(email, fromName);
                MailAddress objto = new MailAddress(sToEmail, sToName);

                MailMessage msgMail = new MailMessage(objFrom, objto);

                // Dim objCC As MailAddress = New MailAddress(ConfigurationManager.AppSettings("CCEmail").ToString)

                //  msgMail.CC.Add(objCC)

                MailAddress objBCC = new MailAddress(ConfigurationManager.AppSettings.Get("BCCEmail").ToString());

                msgMail.Bcc.Add(objBCC);
                msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                msgMail.Priority = MailPriority.High;

                msgMail.IsBodyHtml = sBodyHTML;
                msgMail.Subject = ConfigurationManager.AppSettings.Get("Subject") + " " + sSubject;
                msgMail.Body = sBody;

                if (sAttach == true)
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(FileAttach);
                    msgMail.Attachments.Add(attachment);
                }

                System.Net.Mail.SmtpClient objSMTP = new System.Net.Mail.SmtpClient();

                objSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;


                objSMTP.UseDefaultCredentials = false;
                

                objSMTP.Credentials = new System.Net.NetworkCredential(user, password);

                objSMTP.Host = server;


                objSMTP.Port = Convert.ToInt32(port);
                objSMTP.EnableSsl = ssl;
                objSMTP.Send(msgMail);

                return true;

            }
            catch (Exception ex)
            {

                ExceptionManager.DoLogAndGetFriendlyMessageForException(ex);
                return false;
            }

        }


    }
}