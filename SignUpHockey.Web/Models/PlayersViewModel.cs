using SignUpHockey.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignUpHockey.Web.Models
{
    public class PlayersViewModel
    {
        public IEnumerable<Game> Games { get; set; }
    }
}