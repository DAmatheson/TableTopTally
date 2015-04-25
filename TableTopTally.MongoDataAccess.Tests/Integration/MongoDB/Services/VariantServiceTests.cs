using System.Collections.Generic;
using System.Linq;
using TableTopTally.MongoDataAccess.Services;
using TableTopTally.DataModels.Models;
using MongoDB.Bson;
using NUnit.Framework;

namespace TableTopTally.Tests.Integration.MongoDB.Services
{
    [TestFixture]
    class VariantServiceTests : BaseMongoServiceTests<GameVariantService, GameVariant>
    {
        protected override GameVariantService GetService()
        {
            return new GameVariantService();
        }

        protected override GameVariant CreateEntity(string idString)
        {
            return new GameVariant
            {
                GameId = new ObjectId("53e3a8ad6c46bc0c80ea13b2"),
                Id = new ObjectId(idString),
                Name = "GameVariant",
                TrackScores = true,
                Url = "FakeUrl",
                ScoreItems = new List<ScoreItem>
                {
                    new ScoreItem
                    {
                        Name = "a",
                        Description = "a"
                    }
                }
            };
        }

        [Test]
        public void Edit_IdNotInDb_ReturnsFalse()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariantService service = GetService();

            bool result = service.Edit(entity);

            Assert.IsFalse(result);
        }

        [Test]
        public void Edit_IdInDb_ReturnsTrue()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariantService service = GetService();

            AddEntityToCollection(entity, service);

            entity.Name = "UpdatedVariant";

            // Act
            bool result = service.Edit(entity);

            Assert.IsTrue(result);
        }

        [Test]
        public void Edit_IdInDb_DoesNotDuplicateScoreItems()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariantService service = GetService();

            AddEntityToCollection(entity, service);
            entity.ScoreItems.Add(new ScoreItem { Name = "b", Description = "b" });
            service.Edit(entity);

            // Act
            GameVariant retrieved = service.FindById(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.ScoreItems.Count, Is.EqualTo(entity.ScoreItems.Count));
        }

        [Test]
        public void FindById_AfterEditOfVariant_SuccessfullyDeserializesVariant()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariantService service = GetService();

            AddEntityToCollection(entity, service);
            entity.Name = "a";
            service.Edit(entity);

            // Act
            GameVariant retrieved = service.FindById(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.Name, Is.EqualTo(entity.Name));
        }

        [Test]
        public override void FindById_IdInCollection_ReturnsMatchingEntity()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariantService service = GetService();

            AddEntityToCollection(entity, service);

            // Act
            GameVariant retrieved = service.FindById(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.GameId, Is.EqualTo(entity.GameId));
            Assert.That(retrieved.Name, Is.EqualTo(entity.Name));
            Assert.That(retrieved.ScoreItems.Count, Is.EqualTo(entity.ScoreItems.Count));
        }

        [Test]
        public void FindGameVariants_EmptyCollection_ReturnsEmptyEnumerable()
        {
            GameVariantService service = GetService();

            // Act
            IEnumerable<GameVariant> retrievedVariants = service.FindGameVariants(new ObjectId(VALID_STRING_OBJECT_ID));

            Assert.IsNotNull(retrievedVariants);
            Assert.That(retrievedVariants, Is.Empty);
        }

        [Test]
        public void FindGameVariants_EmptyId_ReturnsEmptyEnumerable()
        {
            GameVariantService service = GetService();

            // Act
            IEnumerable<GameVariant> retrievedVariants = service.FindGameVariants(ObjectId.Empty);

            Assert.IsNotNull(retrievedVariants);
            Assert.That(retrievedVariants, Is.Empty);
        }

        [Test]
        public void FindGameVariants_GameIdInDb_ReturnsMatchingVariants()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariant entity2 = CreateEntity("54a4290ed968bc127cdeaf2e");
            GameVariantService service = GetService();

            AddEntityToCollection(entity, service);
            AddEntityToCollection(entity2, service);

            // Act
            IEnumerable<GameVariant> retrievedVariants = service.FindGameVariants(entity.GameId);

            Assert.IsNotNull(retrievedVariants);

            List<GameVariant> variantsList = retrievedVariants.ToList();

            Assert.That(variantsList.Count, Is.EqualTo(2));
            Assert.That(variantsList[0].Id, Is.EqualTo(entity.Id));
            Assert.That(variantsList[1].Id, Is.EqualTo(entity2.Id));
        }
    }
}
