using SignUpHockey.Data;
using SignUpHockey.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}