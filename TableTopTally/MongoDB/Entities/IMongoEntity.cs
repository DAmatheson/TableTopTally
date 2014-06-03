using MongoDB.Bson;

namespace TableTopTally.MongoDB.Entities
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}
