/* PlaySession.cs
 * 
 * Purpose: Class for play sessions
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */

using System;
using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.DataModels.MongoDB.Entities;

namespace TableTopTally.DataModels.Models
{
    /// <summary>
    /// A play session of a game variant
    /// </summary>
    public class PlaySession : MongoEntity
    {
        public PlaySession() { }

        /// <summary>
        /// The date of the session
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// A collection of all the Round's in the session
        /// </summary>
        public IList<Round> Rounds { get; set; }

        /// <summary>
        /// The GameId for the session's game
        /// </summary>
        public ObjectId GameId { get; set; }

        /// <summary>
        /// The VariantId for the session's game variant
        /// </summary>
        public ObjectId VariantId { get; set; }

        /// <summary>
        /// The Id of the game group for the session
        /// </summary>
        public ObjectId GameGroupId { get; set; }

        /// <summary>
        /// A collection of all the Round's players
        /// </summary>
        /// <remarks>Unsure: IEnumerable because players won't be added later?</remarks>
        public IList<Player> Players { get; set; }

        /// <summary>
        /// The Session's overall rankings
        /// </summary>
        public IList<Ranking> Ranks { get; set; }
    }
}