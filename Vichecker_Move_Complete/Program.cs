using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Vichecker_Move_Complete
{
    class Program
    {
        static string[] srchDirectories = { @"\\wlsnews-nas01\Vantage\Vidchecker\as11\Corrected",
                                            @"\\wlsnews-nas01\Vantage\Vidchecker\as11\success",
                                            @"\\wlsnews-nas01\Vantage\Vidchecker\as11\part_corrected",
                                            @"\\wlsnews-nas01\Vantage\Vidchecker\as11\warning"};
        
        //@"\\wlsnews-nas01\Vantage\Vidchecker\as11\Fail"    ,  @"\\wlsnews-nas01\Vantage\Vidchecker\as11\input"

        static void Main(string[] args)
        {
            string path = Path.GetTempFileName();
            //path = @"I:\Archive\Archive Progs\I2660108s.stl";
            path = args[0];
            string filenamenoext = Path.GetFileNameWithoutExtension(path);
            string filepath = Path.GetDirectoryName(path);
            string outpath = filepath + "\\Output\\" + filenamenoext + "x.stl";
            Debug.WriteLine(outpath);
            SmtpClient client = new SmtpClient(args[0]);
            client.Host = "10.211.6.226";
            // Specify the e-mail sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress("keith.jones@itv.com",
               "Keith " + (char)0xD8 + " Jones",
            System.Text.Encoding.UTF8);
            // Set destinations for the e-mail message.
            MailAddress to = new MailAddress("keith.jones@itv.com");
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = "This is a test e-mail message sent by an application. ";
            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = args[0] + someArrows;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            client.Send(message);
            client.Dispose();
            message.Dispose();
            DirSearch2(@"\\wlsnews-nas01\Vantage\Vidchecker\as11");
            Debug.WriteLine("Goodbye.");

            string S4Cpattern1 = @"^[iI]";
            Match m = Regex.Match(args[0], S4Cpattern1, RegexOptions.IgnoreCase);
            if (m.Success) { }
        }

        static void DirSearch2(string sDir)
        {
            try
            {
                foreach (string d in srchDirectories)
                {
                    foreach (string f in Directory.GetFiles(d,"*.mxf"))
                    {
                        Debug.WriteLine(f);
                    }
                    //DirSearch2(d);
                }
            }
            catch (System.Exception excpt)
            {
                Debug.WriteLine(excpt.Message);
            }
        }
    }
}
