using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.MongoDB.Services
{
    public abstract class MongoService<T> : IMongoService<T> where T : IMongoEntity
    {
        /// <summary>
        ///     MongoCollection for type T
        /// </summary>
        protected readonly MongoCollection<T> collection;

        /// <summary>
        ///     Initializes collection with the MongoCollection for type T
        /// </summary>
        protected MongoService()
        {
            collection = MongoHelper.GetTableTopCollection<T>();
        }

        /// <summary>
        ///     Creates a document in the database
        /// </summary>
        /// <param name="entity">Entity to be created</param>
        /// <returns>Returns a bool representing if the creation completed successfully</returns>
        public virtual bool Create(T entity)
        {
            return collection.Insert(entity).HasLastErrorMessage;
        }

        /// <summary>
        ///     Removes the document from the database
        /// </summary>
        /// <param name="id">ObjectId of the document to remove</param>
        /// <returns>Returns a bool representing if the deletion completed successfully</returns>
        public virtual bool Delete(ObjectId id)
        {
            return collection.Remove(Query.EQ("_id", id), RemoveFlags.Single).HasLastErrorMessage;
        }

        /// <summary>
        ///     Gets a document by its ObjectId
        /// </summary>
        /// <param name="id">ObjectId of the document to find</param>
        /// <returns>A deserialization of the document to a T object</returns>
        public virtual T GetById(ObjectId id)
        {
            return collection.Find(Query.EQ("_id", id)).Single();
        }
    }
}