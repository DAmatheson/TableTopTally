using System;
using System.IO;
using MongoDB.Bson;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using TableTopTally.Helpers;
using TableTopTally.Tests.TestingHelpers;

namespace TableTopTally.Tests.Helpers
{
    [TestFixture]
    public class ObjectIdJsonConverterTest
    {
        private const string STRING_OBJECT_ID = "53e3a8ad6c46bc0c80ea13b2";
        private const string VALID_JSON = "{ 'id': '53e3a8ad6c46bc0c80ea13b2' }";
        private const string INVALID_JSON = "{ }";

        private ObjectIdJsonConverter CreateObjectIdJsonConverter()
        {
            return new ObjectIdJsonConverter();
        }

        [Test]
        public void ReadJson_WithValidObjectId_ReturnsParsedObjectId()
        {
            var converter = CreateObjectIdJsonConverter();
            var serializer = new JsonSerializer();
            var reader = new Mock<JsonReader>();
            reader.SetupGet(t => t.Value).Returns(STRING_OBJECT_ID);

            var result = converter.ReadJson(reader.Object, typeof(ObjectId), null, serializer) as ObjectId?;

            Assert.IsNotNull(result);
            Assert.That(new ObjectId(STRING_OBJECT_ID), Is.EqualTo(result.Value));
        }

        [Test]
        public void ReadJson_WithInvalidObjectId_ReturnsEmptyObjectId()
        {
            var converter = CreateObjectIdJsonConverter();
            var serializer = new JsonSerializer();
            var reader = new Mock<JsonReader>();
            reader.SetupGet(t => t.Value).Returns(" ");

            var result = converter.ReadJson(reader.Object, typeof(ObjectId), null, serializer) as ObjectId?;

            Assert.IsNotNull(result);
            Assert.That(ObjectId.Empty, Is.EqualTo(result.Value));
        }

        [Test]
        public void WriteJson_WhenCalled_ThrowsNotImplementedException()
        {
            var converter = CreateObjectIdJsonConverter();
            var writer = new JsonTextWriter(new StringWriter());
            var serializer = new JsonSerializer();
            var fakeEntity = new FakeMongoEntity();

            Assert.Throws<NotImplementedException>(() => converter.WriteJson(writer, fakeEntity, serializer));
        }

        [Test]
        public void CanWrite_IsFalse()
        {
            var objectIdConverter = CreateObjectIdJsonConverter();

            Assert.IsFalse(objectIdConverter.CanWrite);
        }

        [Test]
        public void CanRead_IsTrue()
        {
            var objectIdConverter = CreateObjectIdJsonConverter();

            Assert.IsTrue(objectIdConverter.CanRead);
        }

        [Test]
        public void CanConvert_TypeofObjectId_ReturnsTrue()
        {
            var objectIdConverter = CreateObjectIdJsonConverter();

            Assert.IsTrue(objectIdConverter.CanConvert(typeof(ObjectId)));
        }

        // These two tests are closer to integration tests
        [Test(Description = "Test json.NET ObjectId converter with a valid ObjectId")]
        public void ValidObjectId()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(CreateObjectIdJsonConverter()); // Add json -> ObjectId converter
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Act
            var testMongoEntity = JsonConvert.DeserializeObject<FakeMongoEntity>(VALID_JSON, settings);

            Assert.IsNotNull(testMongoEntity.Id);
            Assert.That(testMongoEntity.Id, Is.EqualTo(ObjectId.Parse(STRING_OBJECT_ID)));
        }

        [Test(Description = "Test json.NET ObjectId converter with an invalid ObjectId")]
        public void InvalidObjectId()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(CreateObjectIdJsonConverter());
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Act
            var testMongoEntity = JsonConvert.DeserializeObject<FakeMongoEntity>(INVALID_JSON, settings);

            Assert.IsNotNull(testMongoEntity.Id);
            Assert.That(testMongoEntity.Id, Is.EqualTo(ObjectId.Empty));
        }
    }
}
