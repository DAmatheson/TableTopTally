/* PlayerScore.cs
* 
* Purpose: A class for player's scores
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/ 

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class PlayerScore
    {
        /// <summary>
        /// The PlayerScore's Id
        /// </summary>
        [BsonId]
        public ObjectId PlayerScoreId { get; set; }

        /// <summary>
        /// The Player's Id
        /// </summary>
        public ObjectId PlayerId { get; set; }

        /// <summary>
        /// The Player's score for the round
        /// </summary>
        public float Score { get; set; }
    }
}