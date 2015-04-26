/* MongoService.cs
 * Purpose: Generic MongoDB CRUD service
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.17: Created
 */

using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TableTopTally.DataModels.MongoDB.Entities;

namespace TableTopTally.MongoDataAccess.Services
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
        protected readonly IMongoCollection<T> collection;

        /// <summary>
        /// Initializes collection with the MongoCollection for type <see cref="T"/>
        /// </summary>
        protected MongoService()
        {
            collection = MongoHelper.GetTableTopCollection<T>();
        }

        /// <summary>
        /// Adds a document to the database
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        /// <returns>Returns a bool representing if the creation completed successfully</returns>
        public virtual async Task<bool> AddAsync(T entity)
        {
            bool created;

            try
            {
                // TODO: Figure out best way to ensure the insert succeeded
                //created = !collection.Insert(entity).HasLastErrorMessage;

                await collection.InsertOneAsync(entity);

                created = true;

            }
            catch (MongoWriteException)
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
        public virtual async Task<bool> RemoveAsync(ObjectId id)
        {
            // Note: Other Ways to do the same query
            //var filter = Builders<T>.Filter.Eq("_id", id);
            //var result = await collection.DeleteOneAsync(filter);

            //DeleteResult result = await collection.DeleteOneAsync(item => item.Id == id);

            DeleteResult result = await collection.DeleteOneAsync(Builders<T>.Filter.Eq(item => item.Id, id));

            return result.DeletedCount == 1;
        }

        /// <summary>
        /// Finds a document by its ObjectId. If no match is found, null is returned
        /// </summary>
        /// <param name="id">ObjectId of the document to find</param>
        /// <returns>A deserialization of the document to a <see cref="T"/> object</returns>
        public virtual async Task<T> FindByIdAsync(ObjectId id)
        {
            return await collection.Find(item => item.Id == id).FirstOrDefaultAsync();
        }
    }
}