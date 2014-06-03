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
        /// <param name="creatorId">Objectid for the player who created the session</param>
        /// <param name="players">IEnumerable of type Player containing all the session's players</param>
        public PlaySession(ObjectId gameId, ObjectId variantId, ObjectId creatorId, IEnumerable<Player> players)
        {
            Id = ObjectId.GenerateNewId();
            Date = DateTime.Today;
            Rounds = new List<Round>();

            GameId = gameId;
            VariantId = variantId;
            CreatorId = creatorId;
            Players = players;
        }

        /// <summary>
        /// The date of the session
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// A collection of all the Round's of the session
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
        /// The Id of the player who created the session
        /// </summary>
        public ObjectId CreatorId { get; set; }

        /// <summary>
        /// A collection of all the Round's players
        /// </summary>
        /// <remarks>Unsure: IEnumerable because players won't be added later?</remarks>
        public IEnumerable<Player> Players { get; set; }

        /// <summary>
        /// The Session's overall rankings
        /// </summary>
        public Ranking Ranks { get; set; }
    }
}