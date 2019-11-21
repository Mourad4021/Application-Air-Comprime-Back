using MailKit.Net.Smtp;
using SuiviCompresseur.Notification.Data.Context;
using SuiviCompresseur.Notification.Domain.Interfaces;
using SuiviCompresseur.Notification.Domain.Models;
using SuiviCompresseur.Notification.Domain.Services;
using MimeKit;
using MimeKit.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SuiviCompresseur.Notification.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly Notification_context _context;
        private readonly IEmailConfiguration _emailConfiguration;
        public NotificationRepository(Notification_context notification_Context, IEmailConfiguration emailConfiguration)
        {
            _context = notification_Context;
            _emailConfiguration = emailConfiguration;
        }
        public IEnumerable<EmailFrom> GetNotifications(string address)
        {
            var Notification = _context.EmailTos.Where(t => t.ToAddresses == address && t.Seen == false).Select(v => v.IdMail).ToList();

            List<EmailFrom> Notification1 = new List<EmailFrom>();

            foreach (var to in Notification)
            {
                EmailFrom Notification2 = new EmailFrom();
                Notification2 = _context.EmailFroms.Find(to);
                if (Notification2.ExceptionMessage == "0")
                {
                    Notification1.Add(Notification2);
                }
                
            }

            return Notification1;
        }

        public string NotificationSeen(Guid Id, string address)
        {
            var Notification = _context.EmailTos.Where(c => c.IdMail == Id && c.ToAddresses == address).FirstOrDefault();
            if (Notification != null)
            {
                Notification.Seen = true;

                _context.SaveChanges();
                return "Update Done";
            }
            return "Notification not exist";
        }

        public string Send(EmailMessage emailMessage)
        {
            string NameFrom = emailMessage.FromAddresses.Substring(1, emailMessage.FromAddresses.IndexOf("/") - 1);
            string NameTo = emailMessage.ToAddresses.Substring(1, emailMessage.ToAddresses.IndexOf("/") - 1);
            var message = new MimeMessage();

            try
            {

   
                message.From.Add(new MailboxAddress(NameFrom, emailMessage.FromAddresses));
                message.To.Add(new MailboxAddress(NameTo, emailMessage.ToAddresses));
                message.Subject = emailMessage.Subject;
                var bodyBuilder1 = new BodyBuilder();
                bodyBuilder1.HtmlBody = emailMessage.Content;


                var builder = new BodyBuilder();
                var image = builder.LinkedResources.Add(@"./poulinalogo.png");
                image.ContentId = MimeUtils.GenerateMessageId();


                builder.HtmlBody = string.Format(@"<br><br><br>

<table>
  <thead>
    <tr>
    <td>
      {0}
      </td>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>E-Mail : {1}</td>
    </tr>
    <tr>
      <td>Service : Informatique Operationnelle</td>
    </tr>
    <tr>
      <td>Poste : 801249</td>
    </tr>
    <tr>
      <td>GSM : 58278855</td>
    </tr>
</tbody>
<tfoot> 
<center><img src=""cid:{2}""></center>
</tfoot>
</table>",NameFrom, message.From.ToString(), image.ContentId);



                var multipart = new Multipart("mixed");
                multipart.Add(bodyBuilder1.ToMessageBody());
                multipart.Add(builder.ToMessageBody());


                var multipart1 = new Multipart("Attachements");
                message.Body = multipart;



                using (var emailClient = new SmtpClient())
                {


                    emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, false);

                    emailClient.Send(message);
                    emailClient.Disconnect(true);


                    EmailFrom emailFrom = new EmailFrom();

                    emailFrom.FromAddresses = emailMessage.FromAddresses;
        
                    emailFrom.FromName = NameFrom;
                    emailFrom.Subject = emailMessage.Subject;
                    emailFrom.Content = emailMessage.Content;
                    emailFrom.SendDate = DateTime.Now;
                    emailFrom.MessageType = "info";
                    emailFrom.ExceptionMessage = "0";

                    _context.EmailFroms.Add(emailFrom);

                    EmailTo emailTo = new EmailTo();

                    emailTo.IdMail = emailFrom.IdMail;
                    emailTo.Seen = false;
                    emailTo.ToAddresses = emailMessage.ToAddresses;
                    
                    emailTo.ToName = NameTo;
                    emailTo.ReceiveType = "A";

                    _context.EmailTos.Add(emailTo);

                    //if (emailMessage.CcAddresses != "")
                    //{
                    //    EmailTo emailTo2 = new EmailTo();

                    //    emailTo2.IdMail = emailFrom.IdMail;
                    //    emailTo2.Seen = false;
                    //    emailTo2.ToAddresses = emailMessage.CcAddresses;
                    //    string NameCc= emailMessage.CcAddresses.Remove(emailMessage.CcAddresses.IndexOf("/"));
                    //    emailTo2.ToName = NameCc;
                    //    emailTo2.ReceiveType = "Cc";

                    //    _context.EmailTos.Add(emailTo2);
                    //}


                    //if (emailMessage.CccAddresses != "")
                    //{
                    //    EmailTo emailTo3 = new EmailTo();
                    //    emailTo3.IdMail = emailFrom.IdMail;
                    //    emailTo3.Seen = false;
                    //    emailTo3.ToAddresses = emailMessage.CccAddresses;
                    //    string NameCcc = emailMessage.CccAddresses.Remove(emailMessage.CccAddresses.IndexOf("/"));
                    //    emailTo3.ToName = NameCcc;
                    //    emailTo3.ReceiveType = "Ccc";

                    //    _context.EmailTos.Add(emailTo3);
                    //}
                    _context.SaveChanges();
                    return "Mail success";

                }
            }
            catch (Exception ex)
            {

                EmailFrom emailFrom = new EmailFrom();

                emailFrom.FromAddresses = emailMessage.FromAddresses;
                
                emailFrom.FromName = NameFrom;
                emailFrom.Subject = emailMessage.Subject;
                emailFrom.Content = emailMessage.Content;
                emailFrom.SendDate = DateTime.Now;
                emailFrom.MessageType = "info";
                emailFrom.ExceptionMessage = ex.Message;

                _context.EmailFroms.Add(emailFrom);


                EmailTo emailTo = new EmailTo();

                emailTo.IdMail = emailFrom.IdMail;
                emailTo.Seen = false;
                emailTo.ToAddresses = emailMessage.ToAddresses;
               
                emailTo.ToName = NameTo;
                emailTo.ReceiveType = "A";
                _context.EmailTos.Add(emailTo);


                //if (emailMessage.CcAddresses != "")
                //{
                //    EmailTo emailTo2 = new EmailTo();

                //    emailTo2.IdMail = emailFrom.IdMail;
                //    emailTo2.Seen = false;
                //    emailTo2.ToAddresses = emailMessage.CcAddresses;
                //    string NameCc = emailTo2.ToAddresses.Remove(emailTo2.ToAddresses.IndexOf("/"));
                //    emailTo2.ToName = NameCc;
                //    emailTo2.ReceiveType = "Cc";
                //    _context.EmailTos.Add(emailTo2);
                //}

                //if (emailMessage.CccAddresses != "")
                //{
                //    EmailTo emailTo3 = new EmailTo();

                //    emailTo3.IdMail = emailFrom.IdMail;
                //    emailTo3.Seen = false;
                //    emailTo3.ToAddresses = emailMessage.CccAddresses;
                //    string NameCcc = emailTo3.ToAddresses.Remove(emailTo3.ToAddresses.IndexOf("/"));
                //    emailTo3.ToName = NameCcc;
                //    emailTo3.ReceiveType = "Ccc";
                //    _context.EmailTos.Add(emailTo3);
                //}
                _context.SaveChanges();
                return ex.Message;

            }
        }





    }
}
