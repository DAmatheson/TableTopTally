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
        bool Create(T entity);

        bool Delete(ObjectId id);

        T GetById(ObjectId id);
    }
}