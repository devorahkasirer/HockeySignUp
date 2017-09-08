using SignUpHockey.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignUpHockey.Web.Models
{
    public class GameViewModel
    {
        public Game Game { get; set; }
        public bool FullGame { get; set; }
    }
}