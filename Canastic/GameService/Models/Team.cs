using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        public string Name { get; set; }
        public int MyProperty { get; set; }

        public IList<GameTeam> GameTeams { get; set; }


        public IList<TeamPlayer> TeamPlayers { get; set; }
    }
}
