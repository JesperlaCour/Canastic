using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.Models
{
    public class GameFinishedDTO
    {
        public int GameId { get; set; }
        public List<Guid> winners { get; set; }
    }
}
