using System.Collections.Generic;
using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.Models;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Tests.Integration.MongoDB.Services
{
    [TestFixture]
    class VariantServiceTests : BaseMongoServiceTests<VariantService, Variant>
    {
        protected override VariantService GetService()
        {
            return new VariantService();
        }

        protected override Variant CreateEntity(string idString)
        {
            return new Variant
            {
                GameId = new ObjectId("53e3a8ad6c46bc0c80ea13b2"),
                Id = new ObjectId(idString),
                Name = "Variant",
                TrackScores = true,
                Url = "FakeUrl",
                ScoreItems = new List<ScoreItem>
                {
                    new ScoreItem("a", "a")
                }
            };
        }

        [Test]
        public void Edit_WithValidVariantNotInDb_ReturnsFalse()
        {
            Variant entry = CreateEntity(VALID_STRING_OBJECT_ID);
            VariantService service = GetService();

            bool result = service.Edit(entry);

            Assert.IsFalse(result);
        }

        [Test]
        public void Edit_WithValidVariantInDb_ReturnsTrue()
        {
            Variant entry = CreateEntity(VALID_STRING_OBJECT_ID);
            VariantService service = GetService();

            service.Create(entry);

            entry.Name = "UpdatedVariant";

            // Act
            bool result = service.Edit(entry);

            Assert.IsTrue(result);
        }

        [Test]
        public void Edit_WithValidVariantInDb_DoesNotDuplicateScoreItems()
        {
            Variant entry = CreateEntity(VALID_STRING_OBJECT_ID);
            VariantService service = GetService();

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
        public void GetById_AfterEditOfVariant_SuccessfullyDeserializesVariant()
        {
            Variant entry = CreateEntity(VALID_STRING_OBJECT_ID);
            VariantService service = GetService();

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
        public override void GetById_IdInCollection_ReturnsMatchingEntity()
        {
            Variant entry = CreateEntity(VALID_STRING_OBJECT_ID);
            VariantService service = GetService();

            service.Create(entry);

            // Act
            Variant retrieved = service.GetById(entry.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entry.Id));
            Assert.That(retrieved.GameId, Is.EqualTo(entry.GameId));
            Assert.That(retrieved.Name, Is.EqualTo(entry.Name));
            Assert.That(retrieved.ScoreItems.Count, Is.EqualTo(entry.ScoreItems.Count));
        }
    }
}
