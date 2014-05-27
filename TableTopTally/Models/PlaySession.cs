using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class PlaySession
    {
        [BsonId]
        public ObjectId SessionId { get; set; }

        /// <summary>
        /// A collection of all the Round's of the Session
        /// </summary>
        public IEnumerable<Round> Rounds { get; set; }
    }
}