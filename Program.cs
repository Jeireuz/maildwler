using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.MIME
using System;
using System.Collections.Generic;
namespace maildwler
{
class Program
{
    static void Main(string[] args)
    {

while(true){

    Task.Delay(60000).Wait();
        using(Imap imap = new Imap())
        {
            imap.Connect("Ponga aqui su mail"); // Conexion por SSL
            imap.UseBestLogin("Usuario", "Contraseña");
     
            imap.SelectInbox();
            List<long> uids = imap.Search(Flag.Unseen);
            foreach (long uid in uids)
            {
                var eml = imap.GetMessageByUID(uid);
                IMail email = new MailBuilder()
                    .CreateFromEml(eml);
                Console.WriteLine(email.Subject);
     
                foreach (MimeData mime in email.Attachments)
                {
                    mime.Save(@"C:\Mails\" + mime.SafeFileName);
                }
            }
            imap.Close();
        }
}


    }
};
}
