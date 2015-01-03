using System.Collections.Generic;
using System.Globalization;
using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.Models;
using TableTopTally.MongoDB;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Tests.Integration.MongoDB.Services
{
    [TestFixture]
    class VariantServiceTests
    {
        //[TearDown]
        public void DropCollection()
        {
            //Clear db
            var collection = MongoHelper.GetTableTopCollection<Variant>();

            collection.Drop();
        }

        private Variant CreateVariant(bool invalidId = false)
        {
            string id = "54a07c8a4a91a323e83d78d2";

            if (invalidId)
            {
                id = "";
            }
            return new Variant
            {
                GameId = new ObjectId("53e3a8ad6c46bc0c80ea13b2"),
                Id = new ObjectId(id),
                Name = "Variant",
                TrackScores = true,
                Url = "FakeUrl",
                ScoreItems = new List<ScoreItem>
                {
                    new ScoreItem("a", "a")
                }
            };
        }

        private VariantService CreateVariantService()
        {
            return new VariantService();
        }

        [Test]
        public void Edit_WithValidVariantNotInDb_ReturnsFalse()
        {
            Variant entry = CreateVariant();
            VariantService service = CreateVariantService();

            bool result = service.Edit(entry);

            Assert.IsFalse(result);
        }

        [Test]
        public void Edit_WithValidVariantInDb_ReturnsTrue()
        {
            Variant entry = CreateVariant();
            VariantService service = CreateVariantService();

            service.Create(entry);

            entry.Name = "UpdatedVariant";

            // Act
            bool result = service.Edit(entry);

            Assert.IsTrue(result);
        }

        [Test]
        public void Edit_WithValidVariantInDb_DoesNotDuplicateScoreItems()
        {
            Variant entry = CreateVariant();
            VariantService service = CreateVariantService();

            service.Create(entry);
            entry.ScoreItems.Add(entry.ScoreItems[0]);
            service.Edit(entry);

            // Act
            Variant retrieved = service.GetById(entry.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entry.Id));
            Assert.That(retrieved.ScoreItems.Count, Is.EqualTo(entry.ScoreItems.Count));
        }

        [Test]
        public void GetById_AfterEditOfVariant_ReturnsVariant()
        {
            Variant entry = CreateVariant();
            VariantService service = CreateVariantService();

            service.Create(entry);
            entry.Name = "a";
            service.Edit(entry);

            // Act
            Variant retrieved = service.GetById(entry.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entry.Id));
            Assert.That(retrieved.Name, Is.EqualTo(entry.Name));
        }

        [Test]
        public void GetById_WithValidVariantInDb_ReturnsSameVariant()
        {
            Variant entry = CreateVariant();
            VariantService service = CreateVariantService();

            service.Create(entry);

            // Act

            Variant retrieved = service.GetById(entry.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entry.Id));
            Assert.That(retrieved.GameId, Is.EqualTo(entry.GameId));
            Assert.That(retrieved.Name, Is.EqualTo(entry.Name));
            Assert.That(retrieved.TrackScores, Is.EqualTo(entry.TrackScores));
            Assert.That(retrieved.ScoreItems.Count, Is.EqualTo(entry.ScoreItems.Count));
        }
    }
}
