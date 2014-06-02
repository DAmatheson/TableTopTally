using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Entities
{
    public abstract class MongoEntity : IMongoEntity
    {
        /// <summary>
        /// The MongoEntity's Id 
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }
    }
}