/* PlaySession.cs
* 
* Purpose: Class for play sessions
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
 *     Drew Matheson, 2014.05.30: Added constructors, Date, Game Variant Creator Ids, Players, and Ranks
*/

using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class PlaySession
    {
        /// <summary>
        /// Initializes a new instance of the PlaySession class
        /// </summary>
        public PlaySession()
        {
            Date = DateTime.Today;

            // Initialize the session with one round
            Rounds = new List<Round> { new Round() };
        }

        /// <summary>
        /// Initializes a new instance of the PlaySession class and sets GameId and VariantId
        /// </summary>
        /// <param name="gameId">ObjectId of the session's game</param>
        /// <param name="variantId">ObjectId of the session's game variant</param>
        public PlaySession(ObjectId gameId, ObjectId variantId)
        {
            GameId = gameId;
            VariantId = variantId;

            Date = DateTime.Today;

            // Initialize the session with one round
            Rounds = new List<Round> { new Round() };
        }

        /// <summary>
        /// The Session's Id
        /// </summary>
        [BsonId]
        public ObjectId SessionId { get; set; }

        /// <summary>
        /// The date of the session
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// A collection of all the Round's of the Session
        /// </summary>
        public IEnumerable<Round> Rounds { get; set; }

        public ObjectId GameId { get; set; }

        public ObjectId VariantId { get; set; }

        public ObjectId CreatorId { get; set; }

        public IList<Player> Players { get; set; }

        public IList<Player> Ranks { get; set; }
    }
}