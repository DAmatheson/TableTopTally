using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.MongoDB;
using TableTopTally.MongoDB.Entities;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Tests.Integration.MongoDB.Services
{
    public abstract class BaseMongoServiceTests<TService, TEntity> where TService: IMongoService<TEntity> where TEntity : IMongoEntity
    {
        protected abstract TService GetService();

        protected abstract TEntity CreateEntity(string idString);

        protected const string VALID_STRING_OBJECT_ID = "54a07c8a4a91a323e83d78d2";

        protected readonly string EMPTY_OBJECT_ID_STRING = ObjectId.Empty.ToString();

        protected virtual void AddEntityToCollection(TEntity entity, TService service)
        {
            service.Create(entity);
        }

        [SetUp]
        public virtual void SetUp_ClearCollection()
        {
            var collection = MongoHelper.GetTableTopCollection<TEntity>();

            collection.RemoveAll();
        }

        [Test]
        public void Create_ValidEntity_ReturnsTrue()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            bool result = service.Create(entity);

            Assert.IsTrue(result);
        }

        [Test]
        public void Create_EmptyId_HasIdAfterInsert()
        {
            TEntity entity = CreateEntity(EMPTY_OBJECT_ID_STRING);
            TService service = GetService();

            service.Create(entity);

            Assert.That(entity.Id, Is.Not.EqualTo(ObjectId.Empty));
        }

        [Test]
        public void Delete_IdInCollection_ReturnsTrue()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            AddEntityToCollection(entity, service);

            bool result = service.Delete(entity.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void Delete_IdNotInCollection_ReturnsFalse()
        {
            ObjectId id = new ObjectId(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            bool result = service.Delete(id);

            Assert.IsFalse(result);
        }

        [Test]
        public virtual void GetById_IdInCollection_ReturnsMatchingEntity()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            AddEntityToCollection(entity, service);

            TEntity retrieved = service.GetById(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
        }

    }
}
