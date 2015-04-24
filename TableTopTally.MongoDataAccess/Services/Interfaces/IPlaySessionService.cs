/* IPlaySessionService.cs
 * 
 * Purpose: Interface for database services for the PlaySession model
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */

using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.DataModels.Models;

namespace TableTopTally.MongoDataAccess.Services
{
    public interface IPlaySessionService : IMongoService<PlaySession>
    {
        bool Edit(PlaySession session);

        IEnumerable<PlaySession> GetSessions(ObjectId creatorId);
    }
}
