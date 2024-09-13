using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FAV_Site.GmailServicesHelp
{
    public class SmtpEmailService
    {
        public async Task SendEmailAsync(string recipientAddress, string subject, string body)
        {
            // Configuration de l'adresse email de l'expéditeur et du mot de passe des applications
            var fromAddress = new MailAddress("Samikah23@gmail.com", "FAV acheter_vendre");
            var toAddress = new MailAddress(recipientAddress); // Renommé pour éviter le conflit
            const string fromPassword = "mon motde passe app"; // Utilisez le mot de passe des applications généré

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587, // Port pour TLS
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
                try
                {
                    // Envoi de l'email
                    await smtp.SendMailAsync(message);
                    Console.WriteLine("Email envoyé avec succès !");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de l'envoi de l'email : {ex.Message}");
                }
            }
        }
    }
}
