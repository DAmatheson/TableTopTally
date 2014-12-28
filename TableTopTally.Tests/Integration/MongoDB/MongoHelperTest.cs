using MongoDB.Driver;
using NUnit.Framework;
using TableTopTally.Models;
using TableTopTally.MongoDB;

namespace TableTopTally.Tests.Integration.MongoDB
{
    [TestFixture]
    public class MongoHelperTest
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
