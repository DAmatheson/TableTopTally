using TableTopTally.DataModels.MongoDB.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Tests.TestingHelpers
{
    internal class FakeMongoEntity : IMongoEntity
    {
        [BsonId]
        //[CustomValidation(typeof(ObjectIdModelValidator), "IsValid")]
        public ObjectId Id { get; set; }
    }
}
