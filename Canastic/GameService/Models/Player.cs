using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public Guid MongoId { get; set; }

        public IList<TeamPlayer> TeamPlayers { get; set; }
    }
}
