using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.Models
{
    public class GameDTO
    {
        public int GameId { get; set; }

        public bool Finished { get; set; }

        public int Rounds { get; set; }

        public TeamDTO teamOne { get; set; }

        public TeamDTO teamTwo { get; set; }
    }
}
