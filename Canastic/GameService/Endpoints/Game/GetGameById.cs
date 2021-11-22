using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Endpoints.Game
{
    public class GetGameById : BaseEndpoint.WithoutRequest.WithoutResponse
    {

        [HttpGet("/GameByID")]
        [SwaggerOperation(
            Summary = "return all games by gameID"
            )]
        public override ActionResult Handle() //remember [fromBody] to specify when using .WithRequest
        {
            throw new NotImplementedException();
        }
    }
}
