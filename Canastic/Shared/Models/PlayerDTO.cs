using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SharedService.Models
{
    [BsonIgnoreExtraElements]
    public class PlayerShort
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("Name")]
        public string PlayerName { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class PlayerDTO
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("Name")]
        public string PlayerName { get; set; }

        public DateTime PlayedSince { get; set; }

        public int TotalWins { get; set; }
    }
}
