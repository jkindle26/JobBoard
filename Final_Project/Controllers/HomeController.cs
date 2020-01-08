using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Contact()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModels cvm)
        {
            if (ModelState.IsValid)
            {
                string message = $"You have recieved a email from {cvm.Name} " +
                    $"With the subject {(string.IsNullOrEmpty(cvm.Subject) ? "No Subject" : cvm.Subject)} " +
                    $"Please respond to {cvm.Email} <br/>Message:<br/>" +
                    $"<em>{cvm.Message}</em>";

                MailMessage mm = new MailMessage(
                    "jkindle26@gmail.com",
                    "jkindle26@outlook.com",
                    string.IsNullOrEmpty(cvm.Subject) ? "No Subject" : cvm.Subject,
                    message
                    );

                mm.IsBodyHtml = true;
                mm.Priority = MailPriority.High;
                mm.ReplyToList.Add(cvm.Email);

                SmtpClient client = new SmtpClient("mail.kindledev.com");
                client.Port = 8889;
                client.Credentials = new NetworkCredential(
                    "jkindle@kindledev.com", "P@ssw0rd");

                try
                {
                    client.Send(mm);
                }
                catch (Exception e)
                {

                    ViewBag.FailedEmailMessage =
                        $"We're sorry your request was not completed at this time." +
                        $" Please try again later. Error Message: <br/>{e.StackTrace}";
                    return View(cvm);
                }


                return View("Index");



            }//ModelStatIsValid
            return View(cvm);

        }


        public ActionResult Projects()
        {
            return View();
        }

        public ActionResult Team()
        {
            return View();
        }
    }
}