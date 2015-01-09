using System.Collections.Generic;
using System.Linq;
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
        public void Edit_IdNotInDb_ReturnsFalse()
        {
            Variant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            VariantService service = GetService();

            bool result = service.Edit(entity);

            Assert.IsFalse(result);
        }

        [Test]
        public void Edit_IdInDb_ReturnsTrue()
        {
            Variant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            VariantService service = GetService();

            AddEntityToCollection(entity, service);

            entity.Name = "UpdatedVariant";

            // Act
            bool result = service.Edit(entity);

            Assert.IsTrue(result);
        }

        [Test]
        public void Edit_IdInDb_DoesNotDuplicateScoreItems()
        {
            Variant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            VariantService service = GetService();

            AddEntityToCollection(entity, service);
            entity.ScoreItems.Add(new ScoreItem { Name = "b", Description = "b" });
            service.Edit(entity);

            // Act
            Variant retrieved = service.FindById(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.ScoreItems.Count, Is.EqualTo(entity.ScoreItems.Count));
        }

        [Test]
        public void FindById_AfterEditOfVariant_SuccessfullyDeserializesVariant()
        {
            Variant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            VariantService service = GetService();

            AddEntityToCollection(entity, service);
            entity.Name = "a";
            service.Edit(entity);

            // Act
            Variant retrieved = service.FindById(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.Name, Is.EqualTo(entity.Name));
        }

        [Test]
        public override void FindById_IdInCollection_ReturnsMatchingEntity()
        {
            Variant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            VariantService service = GetService();

            AddEntityToCollection(entity, service);

            // Act
            Variant retrieved = service.FindById(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.GameId, Is.EqualTo(entity.GameId));
            Assert.That(retrieved.Name, Is.EqualTo(entity.Name));
            Assert.That(retrieved.ScoreItems.Count, Is.EqualTo(entity.ScoreItems.Count));
        }

        [Test]
        public void FindGameVariants_EmptyCollection_ReturnsEmptyEnumerable()
        {
            VariantService service = GetService();

            // Act
            IEnumerable<Variant> retrievedVariants = service.FindGameVariants(new ObjectId(VALID_STRING_OBJECT_ID));

            Assert.IsNotNull(retrievedVariants);
            Assert.That(retrievedVariants, Is.Empty);
        }

        [Test]
        public void FindGameVariants_EmptyId_ReturnsEmptyEnumerable()
        {
            VariantService service = GetService();

            // Act
            IEnumerable<Variant> retrievedVariants = service.FindGameVariants(ObjectId.Empty);

            Assert.IsNotNull(retrievedVariants);
            Assert.That(retrievedVariants, Is.Empty);
        }

        [Test]
        public void FindGameVariants_GameIdInDb_ReturnsMatchingVariants()
        {
            Variant entity = CreateEntity(VALID_STRING_OBJECT_ID);
            Variant entity2 = CreateEntity("54a4290ed968bc127cdeaf2e");
            VariantService service = GetService();

            AddEntityToCollection(entity, service);
            AddEntityToCollection(entity2, service);

            // Act
            IEnumerable<Variant> retrievedVariants = service.FindGameVariants(entity.GameId);

            Assert.IsNotNull(retrievedVariants);

            List<Variant> variantsList = retrievedVariants.ToList();

            Assert.That(variantsList.Count, Is.EqualTo(2));
            Assert.That(variantsList[0].Id, Is.EqualTo(entity.Id));
            Assert.That(variantsList[1].Id, Is.EqualTo(entity2.Id));
        }
    }
}
