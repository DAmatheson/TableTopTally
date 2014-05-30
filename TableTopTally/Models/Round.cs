/* Round.cs
* 
* Purpose: Class for rounds
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/ 

using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class Round
    {
        /// <summary>
        /// The Round's Id
        /// </summary>
        [BsonId]
        public ObjectId RoundId { get; set; }

        /// <summary>
        /// A collection of all the PlayerScore's for the round
        /// </summary>
        public IEnumerable<PlayerScore> Scores { get; set; }
    }
}