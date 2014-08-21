using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TableTopTally.Helpers;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Tests.Helpers
{
    [TestClass]
    public class ObjectIdConverterTest
    {
        private const string STRING_OBJECT_ID = "53e3a8ad6c46bc0c80ea13b2";
        private const string VALID_JSON = "{ 'id': '53e3a8ad6c46bc0c80ea13b2' }";
        private const string INVALID_JSON = "{ }";

        private class TestMongoEntity : MongoEntity { } // Class with only a ObjectId Id property

        [TestMethod]
        public void ValidObjectId()
        {
            var settings = new JsonSerializerSettings();

            settings.Converters.Add(new ObjectIdConverter()); // Add json -> ObjectId converter
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var testMongoEntity = JsonConvert.DeserializeObject<TestMongoEntity>(VALID_JSON, settings);

            Assert.IsNotNull(testMongoEntity.Id);
            Assert.IsInstanceOfType(testMongoEntity.Id, typeof(ObjectId));
            Assert.AreEqual(ObjectId.Parse(STRING_OBJECT_ID), testMongoEntity.Id);
        }

        [TestMethod]
        public void InvalidObjectId()
        {
            var settings = new JsonSerializerSettings();

            settings.Converters.Add(new ObjectIdConverter()); // Add json -> ObjectId converter
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            var testMongoEntity = JsonConvert.DeserializeObject<TestMongoEntity>(INVALID_JSON, settings);

            Assert.IsNotNull(testMongoEntity.Id);
            Assert.IsInstanceOfType(testMongoEntity.Id, typeof(ObjectId));
            Assert.AreEqual(ObjectId.Empty, testMongoEntity.Id);
        }

        [TestMethod]
        public void Settings()
        {
            var objectIdConverter = new ObjectIdConverter();

            Assert.IsFalse(objectIdConverter.CanWrite);
            Assert.IsTrue(objectIdConverter.CanRead);
            Assert.IsTrue(objectIdConverter.CanConvert(typeof(ObjectId)));
        }
    }
}
