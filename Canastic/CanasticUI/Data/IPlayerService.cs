using SharedService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanasticUI.Data
{
    public interface IPlayerService
    {
        Task<List<PlayerDTO>> GetPlayers();
    }
}