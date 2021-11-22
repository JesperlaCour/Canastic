using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class Game
    {
        public int GameId { get; set; }

        public bool Finished { get; set; }

        public int Rounds { get; set; }

        public int MyProperty { get; set; }

        public IList<GameTeam> GameTeams { get; set; }

    }
}
