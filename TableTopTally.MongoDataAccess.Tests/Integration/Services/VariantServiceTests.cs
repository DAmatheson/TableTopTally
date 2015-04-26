using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.DataModels.Models;
using TableTopTally.MongoDataAccess.Services;

namespace MongoDataAccess.Tests.Integration.Services
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
        public async Task Edit_IdNotInDb_ReturnsFalse()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariantService service = GetService();

            bool result = await service.EditAsync(entity);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task Edit_IdInDb_ReturnsTrue()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariantService service = GetService();

            await AddEntityToCollection(entity, service);

            entity.Name = "UpdatedVariant";

            // Act
            bool result = await service.EditAsync(entity);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Edit_IdInDb_DoesNotDuplicateScoreItems()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariantService service = GetService();

            await AddEntityToCollection(entity, service);
            entity.ScoreItems.Add(new ScoreItem { Name = "b", Description = "b" });
            await service.EditAsync(entity);

            // Act
            GameVariant retrieved = await service.FindByIdAsync(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.ScoreItems.Count, Is.EqualTo(entity.ScoreItems.Count));
        }

        [Test]
        public async Task FindById_AfterEditOfVariant_SuccessfullyDeserializesVariant()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariantService service = GetService();

            await AddEntityToCollection(entity, service);
            entity.Name = "a";
            await service.EditAsync(entity);

            // Act
            GameVariant retrieved = await service.FindByIdAsync(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.Name, Is.EqualTo(entity.Name));
        }

        [Test]
        public override async Task FindById_IdInCollection_ReturnsMatchingEntity()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariantService service = GetService();

            await AddEntityToCollection(entity, service);

            // Act
            GameVariant retrieved = await service.FindByIdAsync(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.GameId, Is.EqualTo(entity.GameId));
            Assert.That(retrieved.Name, Is.EqualTo(entity.Name));
            Assert.That(retrieved.ScoreItems.Count, Is.EqualTo(entity.ScoreItems.Count));
        }

        [Test]
        public async Task FindGameVariants_EmptyCollection_ReturnsEmptyEnumerable()
        {
            GameVariantService service = GetService();

            // Act
            IEnumerable<GameVariant> retrievedVariants = await service.FindGameVariantsAsync(new ObjectId(VALID_STRING_OBJECT_ID));

            Assert.IsNotNull(retrievedVariants);
            Assert.That(retrievedVariants, Is.Empty);
        }

        [Test]
        public async Task FindGameVariants_EmptyId_ReturnsEmptyEnumerable()
        {
            GameVariantService service = GetService();

            // Act
            IEnumerable<GameVariant> retrievedVariants = await service.FindGameVariantsAsync(ObjectId.Empty);

            Assert.IsNotNull(retrievedVariants);
            Assert.That(retrievedVariants, Is.Empty);
        }

        [Test]
        public async Task FindGameVariants_GameIdInDb_ReturnsMatchingVariants()
        {
            GameVariant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameVariant entity2 = CreateEntity("54a4290ed968bc127cdeaf2e");
            GameVariantService service = GetService();

            await AddEntityToCollection(entity, service);
            await AddEntityToCollection(entity2, service);

            // Act
            IEnumerable<GameVariant> retrievedVariants = await service.FindGameVariantsAsync(entity.GameId);

            Assert.IsNotNull(retrievedVariants);

            List<GameVariant> variantsList = retrievedVariants.ToList();

            Assert.That(variantsList.Count, Is.EqualTo(2));
            Assert.That(variantsList[0].Id, Is.EqualTo(entity.Id));
            Assert.That(variantsList[1].Id, Is.EqualTo(entity2.Id));
        }
    }
}
