using Microsoft.AspNetCore.Mvc;
using PlayerService.Services;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlayerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        public string DbTable { get; set; }
        public MongoService DbService { get; set; }

        public PlayerController()
        {
            DbTable = "Players";
            DbService = new(DbTable);
        }

        // GET: api/<PlayerController>
        [HttpGet]
        public IEnumerable<PlayerShort> Get()
        {
            var result = DbService.LoadRecords<PlayerShort>(DbTable);
            return result.ToList();
        }

        // GET api/<PlayerController>/5
        [HttpGet("{Guid}")]
        public Player Get(Guid id)
        {
            var result = DbService.LoadRecordByID<Player>(DbTable, id);
            return result;
        }

        // POST api/<PlayerController>
        [HttpPost]
        public void Post([FromBody] Player value)
        {
            DbService.InsertRecord(DbTable, value);            
        }

        // PUT api/<PlayerController>/5
        [HttpPut("{Guid}")]
        public void Put(Guid id, [FromBody] Player value)
        {
            DbService.UpsertRecord(DbTable,value.Id, value);
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{Guid}")]
        public void Delete(Guid id)
        {
            DbService.DeleteRecord<Player>(DbTable, id);
        }
    }
}
