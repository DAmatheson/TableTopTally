using MongoDB.Driver;
using NUnit.Framework;
using TableTopTally.DataModels.Models;
using TableTopTally.MongoDataAccess;

namespace MongoDataAccess.Tests.Integration
{
    [TestFixture]
    public class MongoHelperTests
    {
        [Test(Description = "Test getting a collection from mongo helper")]
        public void GetCollection()
        {
            // Act
            IMongoCollection<Game> collection = MongoHelper.GetTableTopCollection<Game>();

            // Assert
            Assert.IsNotNull(collection);
            Assert.IsInstanceOf<IMongoCollection<Game>>(collection);
            Assert.That(collection.Database.DatabaseNamespace.DatabaseName, Is.EqualTo("testTableTopTally"));
            Assert.That(collection.CollectionNamespace.CollectionName, Is.EqualTo("games"));
        }
    }
}
