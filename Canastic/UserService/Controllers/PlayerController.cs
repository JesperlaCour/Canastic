using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            DbTable = "Players";
            DbService = new(DbTable);
            _logger = logger;
        }

        // GET: api/<PlayerController>
        [HttpGet]
        public IEnumerable<PlayerDTO> Get()
        {
            _logger.LogInformation("Players is returned after request");
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
        [HttpPut]
        public void Put([FromBody] PlayerDTO value)
        {
            DbService.UpsertRecord(DbTable, value.Id, value);
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            DbService.DeleteRecord<PlayerDTO>(DbTable, id);
        }
    }
}
