/* IMongoService.cs
 * 
 * Purpose: Interface for generic mongoDB CRUD actions
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */

using MongoDB.Bson;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.MongoDB.Services
{
    public interface IMongoService<T> where T : IMongoEntity
    {
        bool Add(T entity);

        bool Remove(ObjectId id);

        T FindById(ObjectId id);
    }
}