/* Round.cs
* 
* Purpose: Class for session rounds
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
    public class Round
    {
        private float highScore;

        /// <summary>
        /// The Round's Id
        /// </summary>
        [BsonId]
        public ObjectId RoundId { get; set; }

        /// <summary>
        /// The PlayerId of the round's winner
        /// </summary>
        [BsonIgnoreIfDefault]
        public ObjectId FirstPlaceId { get; set; }

        /// <summary>
        /// The PlayerId of the round's second place finisher
        /// </summary>
        [BsonIgnoreIfDefault]
        public ObjectId SecondPlaceId { get; set; }

        /// <summary>
        /// The PlayerId of the round's third place finisher
        /// </summary>
        [BsonIgnoreIfDefault]
        public ObjectId ThirdPlaceId { get; set; }

        public float HighScore
        {
            get
            {
                // Return the value set from mongoDB or the max score from the scores enumerable
                float score = highScore;

                if (Scores != null && Scores.Any() && score.Equals(0.0f))
                {
                    // Return the value set from mongoDB or the max score from the scores enumerable
                    score = Scores.Max(playerScore => playerScore.Score);
                }

                return score;
            }

            private set
            {
                highScore = value;
            }
        }

        /// <summary>
        /// A collection of all the PlayerScore's for the round
        /// </summary>
        public IList<PlayerScore> Scores { get; set; }
    }
}