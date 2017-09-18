using SignUpHockey.Data;
using SignUpHockey.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SignUpHockey.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddGame(Game game)
        {
            var repo = new Repository(Properties.Settings.Default.ConStr);
            repo.AddGame(game);
            var Notifies = repo.NotifyAll();
            foreach (NotifyMe nm in Notifies)
            {
                //SendEmails(nm.Email, nm.FirstName + " " + nm.LastName);
            }
            return Json(new
            {
                gameDate = game.Date.ToShortDateString()
            });
        }
        public ActionResult Players()
        {
            var repo = new Repository(Properties.Settings.Default.ConStr);
            var pvm = new PlayersViewModel();
            pvm.Games = repo.AllGames();
            return View(pvm);
        }
        [HttpPost]
        public ActionResult Players(int gameId)
        {
            var repo = new Repository(Properties.Settings.Default.ConStr);
            var players = repo.PlayersForGame(gameId);
            return Json(players.Select(p => new {
                firstName = p.FirstName,
                lastName = p.LastName
            }));
        }
        private void SendEmails(string notifyAddress, string notifyName)
        {
            var fromAddress = new MailAddress("fromAddress@gmail.com", "HockeyManager");
            var toAddress = new MailAddress(notifyAddress, notifyName);
            const string fromPassword = "fromPassword";
            const string subject = "Upcoming Hockey Game";
            const string body = "A New Hockey Game was just set up, come and check it out!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
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
                smtp.Send(message);
            }
        }
    }
}