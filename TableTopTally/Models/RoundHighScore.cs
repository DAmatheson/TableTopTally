/* RoundHighScore.cs
 * Purpose: A class for a single Round's various high scores
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.18: Created
 */ 

using System.Collections.Generic;
using MongoDB.Bson;

namespace TableTopTally.Models
{
    /// <summary>
    /// A Round's various high scores
    /// </summary>
    public class RoundHighScore
    {
        /// <summary>
        /// Player id of highest scorer for the round number
        /// </summary>
        public ObjectId ScorerId { get; set; }

        /// <summary>
        /// Highest round score
        /// </summary>
        public double RoundScore { get; set; }

        /// <summary>
        /// The Round number
        /// </summary>
        public int RoundNumber { get; set; }

        /// <summary>
        /// List of all highscores for the variant's ScoreItems
        /// </summary>
        public List<ScoreItemHighScore> ScoreItemsHighScores { get; set; }
    }
}