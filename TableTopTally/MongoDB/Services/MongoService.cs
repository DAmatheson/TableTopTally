/* MongoService.cs
 * Purpose: Generic MongoDB CRUD service
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.17: Created
 */

using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.MongoDB.Services
{
    /// <summary>
    /// Generic service for CRUD actions with MongoDB
    /// </summary>
    /// <typeparam name="T">MongoEntity of type <see cref="T"/></typeparam>
    public abstract class MongoService<T> : IMongoService<T> where T : class, IMongoEntity
    {
        /// <summary>
        /// MongoCollection for type T
        /// </summary>
        protected readonly MongoCollection<T> collection;

        /// <summary>
        /// Initializes collection with the MongoCollection for type <see cref="T"/>
        /// </summary>
        protected MongoService()
        {
            collection = MongoHelper.GetTableTopCollection<T>();
        }

        /// <summary>
        /// Creates a document in the database
        /// </summary>
        /// <param name="entity">Entity to be created</param>
        /// <returns>Returns a bool representing if the creation completed successfully</returns>
        public virtual bool Create(T entity)
        {
            //return !collection.Insert(entity).HasLastErrorMessage;

            bool created;

            try
            {
                collection.Insert(entity);

                created = true;
            }
            catch (MongoDuplicateKeyException)
            {
                created = false;
            }

            return created;
        }

        /// <summary>
        /// Removes the document from the database
        /// </summary>
        /// <param name="id">ObjectId of the document to remove</param>
        /// <returns>Returns a bool representing if the deletion completed successfully</returns>
        public virtual bool Delete(ObjectId id)
        {
            return collection.Remove(Query.EQ("_id", id), RemoveFlags.Single).DocumentsAffected == 1;
        }

        /// <summary>
        /// Gets a document by its ObjectId
        /// </summary>
        /// <param name="id">ObjectId of the document to find</param>
        /// <returns>A deserialization of the document to a <see cref="T"/> object</returns>
        public virtual T GetById(ObjectId id)
        {
            T t;

            try
            {
                t = collection.Find(Query.EQ("_id", id)).Single();
            }
            catch (InvalidOperationException)
            {
                t = null;
            }

            return t;
        }
    }
}