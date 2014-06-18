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
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Models
{
    public class PlaySession : MongoEntity
    {
        public PlaySession() { }

        /// <summary>
        /// Initializes a new instance of the PlaySession class
        /// </summary>
        /// <param name="gameId">ObjectId for the sessions game</param>
        /// <param name="variantId">ObjectId for the sessions game variant</param>
        /// <param name="gameGroupId">Objectid for the group who created the session</param>
        /// <param name="players">IList&lt;Player&gt; containing all the session's players</param>
        public PlaySession(ObjectId gameId, ObjectId variantId, ObjectId gameGroupId, IList<Player> players)
        {
            Id = ObjectId.GenerateNewId();
            Date = DateTime.Today;
            Rounds = new List<Round>();

            GameId = gameId;
            VariantId = variantId;
            GameGroupId = gameGroupId;
            Players = players;
        }

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