/* IPlaySessionService.cs
 * 
 * Purpose: Interface for database services for the PlaySession model
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using TableTopTally.DataModels.Models;

namespace TableTopTally.MongoDataAccess.Services
{
    public interface IPlaySessionService : IMongoService<PlaySession>
    {
        Task<bool> EditAsync(PlaySession session);

        Task<IEnumerable<PlaySession>> GetSessionsAsync(ObjectId creatorId);
    }
}
