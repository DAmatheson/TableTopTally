using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.MongoDB;
using TableTopTally.MongoDB.Entities;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Tests.Integration.MongoDB.Services
{
    public abstract class BaseMongoServiceTests<TService, TEntity> where TService : IMongoService<TEntity>
                                                                   where TEntity : IMongoEntity
    {
        protected abstract TService GetService();

        protected abstract TEntity CreateEntity(string idString);

        protected const string VALID_STRING_OBJECT_ID = "54a07c8a4a91a323e83d78d2";

        protected readonly string EMPTY_OBJECT_ID_STRING = ObjectId.Empty.ToString();

        protected virtual void AddEntityToCollection(TEntity entity, TService service)
        {
            service.Add(entity);
        }

        [SetUp]
        public virtual void SetUp_ClearCollection()
        {
            var collection = MongoHelper.GetTableTopCollection<TEntity>();

            collection.RemoveAll();
        }

        [Test]
        public void Add_ValidEntity_ReturnsTrue()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            bool result = service.Add(entity);

            Assert.IsTrue(result);
        }

        [Test]
        public void Add_IdInCollection_ReturnsFalse()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            AddEntityToCollection(entity, service);

            bool result = service.Add(entity);

            Assert.IsFalse(result);
        }

        [Test]
        public void Add_EmptyId_HasIdAfterInsert()
        {
            TEntity entity = CreateEntity(EMPTY_OBJECT_ID_STRING);
            TService service = GetService();

            service.Add(entity);

            Assert.That(entity.Id, Is.Not.EqualTo(ObjectId.Empty));
        }

        [Test]
        public void Remove_IdInCollection_ReturnsTrue()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            AddEntityToCollection(entity, service);

            bool result = service.Remove(entity.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void Remove_IdNotInCollection_ReturnsFalse()
        {
            ObjectId id = new ObjectId(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            bool result = service.Remove(id);

            Assert.IsFalse(result);
        }

        [Test]
        public virtual void FindById_IdInCollection_ReturnsMatchingEntity()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            AddEntityToCollection(entity, service);

            TEntity retrieved = service.FindById(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
        }

        [Test]
        public void FindById_IdNotInCollection_ReturnsNull()
        {
            ObjectId id = new ObjectId(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            TEntity retrieved = service.FindById(id);

            Assert.IsNull(retrieved);
        }
    }
}
