/* PlayerScore.cs
 * 
 * Purpose: A class for player's scores
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */

using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class PlayerScore
    {
        /// <remarks>Empty constructor used by the mongoDB C# driver to deserialize documents</remarks>
        public PlayerScore() { }

        /// <summary>
        /// Initializes a new instance of the PlayerScore class
        /// </summary>
        /// <param name="playerId">ObjectId for the player the instance represents</param>
        public PlayerScore(ObjectId playerId)
        {
            ScoreItems = new Dictionary<ObjectId, double>();

            PlayerId = playerId;
        }

        /// <summary>
        /// The Player's Id
        /// </summary>
        public ObjectId PlayerId { get; set; }

        /// <summary>
        /// The key is the ObjectId of the scoring item, and the value is the player's score for that item
        /// </summary>
        public IDictionary<ObjectId, double> ScoreItems { get; set; }

        /// <summary>
        /// The Player's score for the round
        /// </summary>
        [BsonIgnore]
        public double ScoreTotal
        {
            get
            {
                double total = 0f;

                if (ScoreItems != null && ScoreItems.Any())
                {
                    total = ScoreItems.Sum(scoreItem => scoreItem.Value);
                }
                return total;
            }
        }
    }
}