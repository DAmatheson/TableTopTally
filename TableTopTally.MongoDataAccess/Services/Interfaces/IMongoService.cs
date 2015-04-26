/* IMongoService.cs
 * 
 * Purpose: Interface for generic mongoDB CRUD actions
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */

using System.Threading.Tasks;
using MongoDB.Bson;
using TableTopTally.DataModels.MongoDB.Entities;

namespace TableTopTally.MongoDataAccess.Services
{
    public interface IMongoService<T> where T : IMongoEntity
    {
        Task<bool> AddAsync(T entity);

        Task<bool> RemoveAsync(ObjectId id);

        Task<T> FindByIdAsync(ObjectId id);
    }
}