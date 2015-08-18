using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailToAyala
{
    class Program
    {
        static void Main(string[] args)
        {
            MailMessage message = new MailMessage
            {
               // From = new MailAddress("gigi.wahle@leadercapital.net", "Giyora Wahle"),
               From = new MailAddress("ran.wahle@sarltd.com"),
                Subject = "HEllo dir sir",
                Body = "Hey Luly, Here is your postacrd",


            };

            message.Attachments.Add(GetAttachment());
            SmtpClient client = new SmtpClient();
           // message.Bcc.Add("ran.wahle@gmail.com");
            message.To.Add("ran.wahle@sarltd.com");
            client.Send(message);
            
        }

        private static Attachment GetAttachment()
        {
            var stream = new MemoryStream();
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write("A POSTACRD FROM HOLMARK");
                stream.Position = 0;
                return new Attachment(stream, "A_POSTCARD_FROM_HOLMARK");
            }
        }
    }
}
