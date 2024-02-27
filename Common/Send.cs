using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common
{
    public class Send
    {
        public string SendMail(string ToEmail, string token)
        {
            string FromEmail = "nallamsunilkumar5@gmail.com";
            MailMessage Message = new MailMessage(FromEmail,ToEmail);
            string MailBody = "the token for the reset password : " + token;
            Message.Subject = "Token Generated for resetting password";
            Message.Body = MailBody.ToString();
            Message.BodyEncoding = Encoding.UTF8;
            Message.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",587);
            NetworkCredential credential = new
                NetworkCredential("nallamsunilkumar5@gmail.com", "mysf whwg dqgm joaj");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;
            smtpClient.Send(Message);
            return ToEmail;
        }
    }
}
