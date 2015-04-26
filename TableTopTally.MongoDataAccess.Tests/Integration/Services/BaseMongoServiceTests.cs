using System.Threading.Tasks;
using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.DataModels.MongoDB.Entities;
using TableTopTally.MongoDataAccess;
using TableTopTally.MongoDataAccess.Services;

namespace MongoDataAccess.Tests.Integration.Services
{
    public abstract class BaseMongoServiceTests<TService, TEntity> where TService : IMongoService<TEntity>
                                                                   where TEntity : IMongoEntity
    {
        protected abstract TService GetService();

        protected abstract TEntity CreateEntity(string idString);

        protected const string VALID_STRING_OBJECT_ID = "54a07c8a4a91a323e83d78d2";

        protected readonly string EMPTY_OBJECT_ID_STRING = ObjectId.Empty.ToString();

        protected readonly BsonDocument EMPTY_FILTER = new BsonDocument();

        protected virtual async Task AddEntityToCollection(TEntity entity, TService service)
        {
            await service.AddAsync(entity);
        }

        [SetUp]
        public virtual async void SetUp_ClearCollection()
        {
            var collection = MongoHelper.GetTableTopCollection<TEntity>();

            await collection.DeleteManyAsync(EMPTY_FILTER);
        }

        [Test]
        public async Task Add_ValidEntity_ReturnsTrue()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            bool result = await service.AddAsync(entity);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Add_IdInCollection_ReturnsFalse()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            await AddEntityToCollection(entity, service);

            bool result = await service.AddAsync(entity);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task Add_EmptyId_HasIdAfterInsert()
        {
            TEntity entity = CreateEntity(EMPTY_OBJECT_ID_STRING);
            TService service = GetService();

            await service.AddAsync(entity);

            Assert.That(entity.Id, Is.Not.EqualTo(ObjectId.Empty));
        }

        [Test]
        public async Task Remove_IdInCollection_ReturnsTrue()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            AddEntityToCollection(entity, service);

            bool result = await service.RemoveAsync(entity.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Remove_IdNotInCollection_ReturnsFalse()
        {
            ObjectId id = new ObjectId(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            bool result = await service.RemoveAsync(id);

            Assert.IsFalse(result);
        }

        [Test]
        public virtual async Task FindById_IdInCollection_ReturnsMatchingEntity()
        {
            TEntity entity = CreateEntity(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            AddEntityToCollection(entity, service);

            TEntity retrieved = await service.FindByIdAsync(entity.Id);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
        }

        [Test]
        public async Task FindById_IdNotInCollection_ReturnsNull()
        {
            ObjectId id = new ObjectId(VALID_STRING_OBJECT_ID);
            TService service = GetService();

            TEntity retrieved = await service.FindByIdAsync(id);

            Assert.IsNull(retrieved);
        }
    }
}
