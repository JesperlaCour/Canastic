using SharedService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace CanasticUI.Data
{
    public class PlayerService : IPlayerService
    {
        private readonly HttpClient client;

        public PlayerService(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = new Uri("http://localhost:14020");
        }

        public async Task<List<PlayerDTO>> GetPlayers()
        {
            try
            {
                var result = await client.GetFromJsonAsync<List<PlayerDTO>>("api/player");
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task UpdatePlayer(PlayerDTO player)
        {
            var result = await client.PutAsJsonAsync("api/player", player);
        }

        public async Task PostPlayer(PlayerDTO player)
        {
            
            var result = await client.PostAsJsonAsync("api/player",player);
        }

        public async Task DeletePlayer(Guid id)
        {
            
            var result = await client.DeleteAsync($"api/player/{id}");
        }



    }
}
