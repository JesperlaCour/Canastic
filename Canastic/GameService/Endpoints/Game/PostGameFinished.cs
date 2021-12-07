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
            Summary = "Gets called when game is finish"
            )]
        public override ActionResult Handle(GameFinishedDTO request)
        {
            using (var _bus = RabbitHutch.CreateBus("host=rabbitmq"))
            {
                _bus.PubSub.Publish(request);
                return Ok();
            }
        }
    }
}
