using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ServeBook_Backend.Models;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using ServeBook_Backend.Data;
using Microsoft.EntityFrameworkCore;


namespace ServeBook_Backend.Aplications.Services.Mail
{
    public class MailRepository
    {
        private readonly Email _emailSettings;

        public MailRepository(IOptions<Email> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void EmailLogIn(string Email, string subject, string body, User user)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", Email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = $"Bienvenid@ a Serve Books {user.name}\n Acabas de iniciar sesión en nuestra página." };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
                client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }

        public void EmailSignUp(string Email, string subject, string body, User user)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", Email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = $"{user.name}, ahora eres uno de nuestros usuarios Serve Books" };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
                client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }

        public void EmailSendLoan(string Email, string subject, string body, User user, Book book, Loan loan, int duration)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", Email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = $"El usuario {user.name}, ha pedido el prestamos del libro {book.title}, Con fecha de entrega del {loan.dateReturn}.\n Prestamo de {duration} días. ¡Autoriza el prestamo!" };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
                client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }

    }
}