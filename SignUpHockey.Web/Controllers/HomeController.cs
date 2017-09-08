using SignUpHockey.Data;
using SignUpHockey.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignUpHockey.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var ivm = new IndexViewModel();
            if (TempData["NewPlayer"] != null)
            {
                ivm.Player = (Player)TempData["NewPlayer"];
            }
            return View(ivm);
        }
        public ActionResult Game()
        {
            var repo = new Repository(Properties.Settings.Default.ConStr);
            var gvm = new GameViewModel();
            var game = repo.UpcomingGame();
            gvm.Game = game;
            if(game != null)
            {
                gvm.FullGame = repo.PlayersForGame(game.Id).Count() == game.MaxPeople;
            }
            return View(gvm);
        }
        public ActionResult SignUp(Player player)
        {
            var repo = new Repository(Properties.Settings.Default.ConStr);
            repo.AddPlayer(player);
            TempData["NewPlayer"] = player;
            return Redirect("/home/index");
        }

    }
}