using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public static class EmailSettings
    {
        // Use SMTP Protocol(simple mail transfer protocol) to send Email
        // we wiil use gmail smtp
        public static void SendEmail(Email model)  //public static void SendEmail(string [] emails)
        {
                                     //SmtpserverName   // smtp Host >> 587 secure and make it encrypt
            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            // Credentials >>  user & pass
            client.Credentials = new NetworkCredential("wkmorsy2022@gmail.com", "mmsueoywrwlgeqiw");
            client.Send("wkmorsy2022@gmail.com",model.To,model.Subject , model.Body);


        }
    }
}
