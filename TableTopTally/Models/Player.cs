using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class Player
    {
        [BsonId]
        public ObjectId PlayerId { get; set; }

        /// <summary>
        /// The Player's Name
        /// </summary>
        public string Name { get; set; }
    }
}