using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RodBrosEntertainment.Models;

namespace RodBrosEntertainment.Controllers
{
    public class ContactController : Controller
    {
        private IConfiguration Configuration;

        public ContactController(IConfiguration iConfiguration)
        {
            Configuration = iConfiguration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Email email = new Email();

            return View(email);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(Email email)
        {
            if (!ModelState.IsValid)
            {
                return View(email);

            }

            var body = $"<p>Email From: {email.FromName} ({email.FromEmail})</p><p>Message:</p><p>{email.Message}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("rodbrosentertainment+contact@gmail.com")); // The part after the + let's you easily set up auto-routing of messages in the email app
            message.From = new MailAddress(email.FromEmail);
            message.Subject = $"Site Contact Form from {email.FromName}";
            message.Body = string.Format(body, email.FromName, email.FromEmail, email.Message);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.UseDefaultCredentials = false;
                var credential = new NetworkCredential
                {
                    UserName = Configuration.GetSection("AppSettings").GetSection("EmailAddress").Value,
                    Password = Configuration.GetSection("AppSettings").GetSection("EmailPass").Value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                try
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
                catch (Exception ex)
                {
                    return View("Error", new ErrorViewModel { Exception = ex });
                }
            }
        }

        [HttpGet]
        public ActionResult Sent()
        {
            return View();
        }
    }
}