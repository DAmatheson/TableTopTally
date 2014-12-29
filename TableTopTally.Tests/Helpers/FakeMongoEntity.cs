using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TableTopTally.Helpers;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Tests.Helpers
{
    internal class FakeMongoEntity : IMongoEntity
    {
        [BsonId]
        //[CustomValidation(typeof(ObjectIdModelValidator), "IsValid")]
        public ObjectId Id { get; set; }
    }
}
