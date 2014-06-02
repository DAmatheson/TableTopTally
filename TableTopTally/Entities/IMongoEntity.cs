using MongoDB.Bson;

namespace TableTopTally.Entities
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}
