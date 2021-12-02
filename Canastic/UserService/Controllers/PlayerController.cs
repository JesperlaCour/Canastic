using Microsoft.AspNetCore.Mvc;
using PlayerService.Services;
using SharedService.Models;
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
        public IEnumerable<PlayerDTO> Get()
        {
            var result = DbService.LoadRecords<PlayerDTO>(DbTable);
            return result.ToList();
        }

        // GET api/<PlayerController>/5
        [HttpGet("{id}")]
        public PlayerDTO Get(Guid id)
        {
            var result = DbService.LoadRecordByID<PlayerDTO>(DbTable, id);
            return result;
        }

        // POST api/<PlayerController>
        [HttpPost]
        public void Post([FromBody] PlayerDTO value)
        {
            DbService.InsertRecord(DbTable, value);            
        }

        // PUT api/<PlayerController>/5
        [HttpPut("{Guid}")]
        public void Put(Guid id, [FromBody] PlayerDTO value)
        {
            DbService.UpsertRecord(DbTable,value.Id, value);
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            DbService.DeleteRecord<PlayerDTO>(DbTable, id);
        }
    }
}
