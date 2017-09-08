using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignUpHockey.Data
{
    public class Repository
    {
        private string _connectionString { get; set; }
        public Repository(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }
        public Game UpcomingGame()
        {
            using (var context = new RepositoryDataContext(_connectionString))
            {
                return context.Games.FirstOrDefault(g => g.Date > DateTime.Now);
            }
        }
        public IEnumerable<Player> PlayersForGame(int gameId)
        {
            using (var context = new RepositoryDataContext(_connectionString))
            {
                return context.Players.Where(p => p.GameId == gameId).ToList();
            }
        }
        public void AddPlayer(Player player)
        {
            using (var context = new RepositoryDataContext(_connectionString))
            {
                context.Players.InsertOnSubmit(player);
                context.SubmitChanges();
            }
        }
        public void AddGame(Game game)
        {
            using (var context = new RepositoryDataContext(_connectionString))
            {
                context.Games.InsertOnSubmit(game);
                context.SubmitChanges();
            }
        }
        public IEnumerable<Game> AllGames()
        {
            using (var context = new RepositoryDataContext())
            {
                return context.Games.ToList();
            }
        }
        public void AddNotify(NotifyMe notify)
        {
            using (var context = new RepositoryDataContext(_connectionString))
            {
                context.NotifyMes.InsertOnSubmit(notify);
                context.SubmitChanges();
            }
        }
        public IEnumerable<NotifyMe> NotifyAll()
        {
            using (var context = new RepositoryDataContext())
            {
                return context.NotifyMes.ToList();
            }
        }
    }
}
