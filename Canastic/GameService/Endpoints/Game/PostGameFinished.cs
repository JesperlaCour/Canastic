using Ardalis.ApiEndpoints;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using SharedService.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace GameService.Endpoints.Game
{
    public class PostGameFinished : BaseEndpoint.WithRequest<GameFinishedDTO>.WithoutResponse
    {
        [HttpPost("/GameFinished")]
        [SwaggerOperation(
            Summary = "return all active games"
            )]
        public override ActionResult Handle(GameFinishedDTO request)
        {
            using (var _bus = RabbitHutch.CreateBus("host=localhost"))
            {
                _bus.PubSub.Publish(request);
                return Ok();
            }
        }
    }
}
