using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using TableTopTally.Helpers;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Tests.Helpers
{
    [TestFixture]
    public class ObjectIdConverterTest
    {
        private const string STRING_OBJECT_ID = "53e3a8ad6c46bc0c80ea13b2";
        private const string VALID_JSON = "{ 'id': '53e3a8ad6c46bc0c80ea13b2' }";
        private const string INVALID_JSON = "{ }";

        private class TestMongoEntity : MongoEntity { } // Class with only a ObjectId Id property

        [Test(Description = "Test json.NET ObjectId converter with a valid ObjectId")]
        public void ValidObjectId()
        {
            // Arrange
            var settings = new JsonSerializerSettings();

            settings.Converters.Add(new ObjectIdConverter()); // Add json -> ObjectId converter
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Act
            var testMongoEntity = JsonConvert.DeserializeObject<TestMongoEntity>(VALID_JSON, settings);

            // Assert
            Assert.IsNotNull(testMongoEntity.Id);
            Assert.IsInstanceOf<ObjectId>(testMongoEntity.Id);
            Assert.That(testMongoEntity.Id, Is.EqualTo(ObjectId.Parse(STRING_OBJECT_ID)));
        }

        [Test(Description = "Test json.NET ObjectId converter with an invalid ObjectId")]
        public void InvalidObjectId()
        {
            // Arrange
            var settings = new JsonSerializerSettings();

            settings.Converters.Add(new ObjectIdConverter()); // Add json -> ObjectId converter
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Act
            var testMongoEntity = JsonConvert.DeserializeObject<TestMongoEntity>(INVALID_JSON, settings);

            // Assert
            Assert.IsNotNull(testMongoEntity.Id);
            Assert.IsInstanceOf<ObjectId>(testMongoEntity.Id);
            Assert.That(testMongoEntity.Id, Is.EqualTo(ObjectId.Empty));
        }

        [Test(Description = "Test json.NET ObjectId converter settings")]
        public void Settings()
        {
            // Arrange
            var objectIdConverter = new ObjectIdConverter();

            // Assert
            Assert.IsFalse(objectIdConverter.CanWrite);
            Assert.IsTrue(objectIdConverter.CanRead);
            Assert.IsTrue(objectIdConverter.CanConvert(typeof(ObjectId)));
        }
    }
}
