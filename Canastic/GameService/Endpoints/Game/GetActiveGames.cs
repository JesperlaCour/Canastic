using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedService.Models;
using GameService.Data;

namespace GameService.Endpoints.Game
{
    public class GetActiveGames : BaseEndpoint.WithoutRequest.WithResponse<IEnumerable<GameDTO>>
    {
        private readonly GameContext _context;

        public GetActiveGames(GameContext context)
        {
            _context = context;
        }

        [HttpGet("/GetActiveGames")]
        [SwaggerOperation(
            Summary = "return all active games"
            )]
        public override ActionResult<IEnumerable<GameDTO>> Handle()
        {
            var games = _context.Games.Where(g => g.Finished == false);
            List<GameDTO> gameDTOs;
            foreach (var game in games)
            {
                //gameDTOs.Add(new GameDTO() { 
                //    Finished=game.Finished,
                //    GameId = game.GameId,
                //    Rounds = game.Rounds,
                //    teamOne = game.GameTeams.
                //})
            }
            throw new NotImplementedException();

        }
    }
}
