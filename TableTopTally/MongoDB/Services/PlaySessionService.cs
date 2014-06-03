/* PlaySessionService.cs
 * 
 * Purpose: A class with methods for CRUDing play sessions to mongoDB
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.30: Created. Very basic outline
 */

using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TableTopTally.Helpers;
using TableTopTally.Models;

namespace TableTopTally.MongoDB.Services
{
    /// <summary>
    /// Provides methods for CRUDing play sessions to a mongoDB database
    /// </summary>
    public class PlaySessionService
    {
        private readonly MongoCollection<PlaySession> sessionCollection;

        public PlaySessionService()
        {
            sessionCollection = MongoHelper.GetTableTopCollection<PlaySession>();
        }

        /// <summary>
        /// Creates a session in the database
        /// </summary>
        /// <param name="session">Session to be created</param>
        /// <returns>Returns a bool representing if the creation completed successfully</returns>
        public bool Create(PlaySession session)
        {
            return !sessionCollection.Insert(session).HasLastErrorMessage;
        }

        /// <summary>
        /// Updates the session in the database
        /// </summary>
        /// <param name="session">Session representing the session to be update</param>
        /// <returns>A bool representing if the edit completed successfully</returns>
        public bool Edit(PlaySession session)
        {
            return !sessionCollection.Update(
                Query.EQ("_id", session.Id),
                Update.Set("Date", session.Date).PushEachWrapped("Rounds", session.Rounds)).
                HasLastErrorMessage;
        }

        /// <summary>
        /// Removes the session from the database
        /// </summary>
        /// <param name="sessionId">ObjectId of the session to remove</param>
        /// <returns>Returns a bool representing if the deletion completed successfully</returns>
        public bool Delete(ObjectId sessionId)
        {
            return !sessionCollection.Remove(Query.EQ("_id", sessionId), RemoveFlags.Single).
                HasLastErrorMessage;
        }

        public IEnumerable<PlaySession> GetSessions(ObjectId creatorId)
        {
            return sessionCollection.Find(Query.EQ("CreatorId", creatorId));
        }
 
    }
}