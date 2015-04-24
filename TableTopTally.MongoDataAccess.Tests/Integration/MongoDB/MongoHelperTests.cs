using TableTopTally.MongoDataAccess;
using TableTopTally.DataModels.Models;
using MongoDB.Driver;
using NUnit.Framework;

namespace TableTopTally.Tests.Integration.MongoDB
{
    [TestFixture]
    public class MongoHelperTests
    {
        [Test(Description = "Test getting a collection from mongo helper")]
        public void GetCollection()
        {
            // Act
            MongoCollection<Game> collection = MongoHelper.GetTableTopCollection<Game>();

            // Assert
            Assert.IsNotNull(collection);
            Assert.IsInstanceOf<MongoCollection<Game>>(collection);
            Assert.That(collection.Database.Name, Is.EqualTo("testTableTopTally"));
            Assert.That(collection.Name, Is.EqualTo("games"));
        }
    }
}
