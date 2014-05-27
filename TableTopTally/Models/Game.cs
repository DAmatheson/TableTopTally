using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class Game
    {
        [ScaffoldColumn(false)]
        [BsonId]
        public ObjectId GameId { get; set; }

        /// <summary>
        /// The Game's Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The Url for the Game
        /// </summary>
        [ScaffoldColumn(false)]
        public string Url { get; set; }

        /// <summary>
        /// A collection of all of the Game's Variants
        /// </summary>
        public IEnumerable<Variant> Variants { get; set; }
    }
}