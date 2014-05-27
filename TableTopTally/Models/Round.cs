using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class Round
    {
        [BsonId]
        public ObjectId RoundId { get; set; }

        /// <summary>
        /// A collection of all the PlayerScore's for the round
        /// </summary>
        public IEnumerable<PlayerScore> Scores { get; set; }
    }
}