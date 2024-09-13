using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Gmail.v1.Data;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Util.Store;
using System.Net.Mail;
namespace FAV_Site.GmailServicesHelp
{
    public class GmailServiceHelper
    {
        private static string[] Scopes = { GmailService.Scope.GmailSend };
        private static string ApplicationName = "FAV";
        //azertyUIOPQSDFhgklm1269742;;,!wxVBN?
        // AWS nom : Samikah et mdp :AMAZONamazon4458;;12eoM
        // mot de passe application moins securise : gnum uvma enkw xvkp   ET nom :  FAV_acheter_vendre_2024:;44Ma
        public static GmailService GetGmailService()
        {
            GoogleCredential credential;
            using (var stream = new FileStream("wwwroot/faniry-acheter-vendre-18ab8003c7fa.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                            .CreateScoped(Scopes)
                            .CreateWithUser("Samikah23@gmail.com"); // votre email Gmail
            }

            return new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public static async Task SendEmailAsync(GmailService service, string to, string subject, string bodyText)
        {
            var message = new AE.Net.Mail.MailMessage
            {
                From = new MailAddress("Samikah23@gmail.com"),
                Subject = subject,
                Body = bodyText
            };
            message.To.Add(new MailAddress(to));

            using (var stream = new MemoryStream())
            {
                message.Save(stream);
                var msg = new Google.Apis.Gmail.v1.Data.Message
                {
                    Raw = Base64UrlEncode(stream.ToArray())
                };

                await service.Users.Messages.Send(msg, "me").ExecuteAsync();
            }
        }

        private static string Base64UrlEncode(byte[] input)
        {
            var base64 = Convert.ToBase64String(input);
            return base64
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
    }
}
