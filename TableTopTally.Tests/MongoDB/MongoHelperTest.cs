using MongoDB.Driver;
using NUnit.Framework;
using TableTopTally.Models;
using TableTopTally.MongoDB;

namespace TableTopTally.Tests.MongoDB
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
        }
    }
}
