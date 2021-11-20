using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class GameTeam

    {
        public int Points { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
