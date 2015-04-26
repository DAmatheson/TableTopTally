/* PlaySessionService.cs
 * 
 * Purpose: A class with methods for CRUDing play sessions to mongoDB
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.30: Created. Very basic outline
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TableTopTally.DataModels.Models;

namespace TableTopTally.MongoDataAccess.Services
{
    /// <summary>
    /// Provides methods for CRUDing play sessions to a mongoDB database
    /// </summary>
    public class PlaySessionService : MongoService<PlaySession>, IPlaySessionService
    {
        /// <summary>
        /// Updates the session in the database
        /// </summary>
        /// <param name="session">Session representing the session to be update</param>
        /// <returns>A bool representing if the edit completed successfully</returns>
        public async Task<bool> EditAsync(PlaySession session)
        {
            UpdateResult result = await collection.
                UpdateOneAsync(
                    Builders<PlaySession>.Filter.
                        Eq(ps => ps.Id, session.Id),
                    Builders<PlaySession>.Update.
                        Set(ps => ps.Date, session.Date).
                        Set(ps => ps.Rounds, session.Rounds));

            return result.ModifiedCount == 1;
        }

        /// <summary>
        /// Removes the session from the database
        /// </summary>
        /// <param name="sessionId">ObjectId of the session to remove</param>
        /// <returns>Returns a bool representing if the deletion completed successfully</returns>
        public async override Task<bool> RemoveAsync(ObjectId sessionId)
        {
            DeleteResult result = await collection.DeleteOneAsync(Builders<PlaySession>.Filter.Eq(ps => ps.Id, sessionId));

            return result.DeletedCount == 1;
        }

        public async Task<IEnumerable<PlaySession>> GetSessionsAsync(ObjectId creatorId)
        {
            return await collection.
                Find(Builders<PlaySession>.Filter.Eq(ps => ps.GameGroupId, creatorId)).
                ToListAsync();
        }
 
    }
}