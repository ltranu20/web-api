﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GirafRest.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender
    {
        private readonly EmailConfig _emailConfig;
        private readonly ILogger _logger;

        public AuthMessageSender(IOptions<EmailConfig> emailConfig, ILoggerFactory loggerFactory)
        {
            _emailConfig = emailConfig.Value;
            _logger = loggerFactory.CreateLogger("EmailSender");
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_emailConfig.FromName, _emailConfig.FromAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = message };

            using (var client = new SmtpClient())
            {
                client.LocalDomain = _emailConfig.LocalDomain;

                await client.ConnectAsync(_emailConfig.MailServerAddress,
                    int.Parse(_emailConfig.MailServerPort), SecureSocketOptions.None).ConfigureAwait(false);
                await client.AuthenticateAsync(new NetworkCredential(_emailConfig.UserId, _emailConfig.UserPassword));
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
