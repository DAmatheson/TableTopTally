/* ScoreItemHighScore.cs
 * Purpose: A class for a single ScoreItem highscore
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.18: Created
 */

using MongoDB.Bson;

namespace TableTopTally.DataModels.Models
{
    /// <summary>
    /// A single ScoreItem highscore
    /// </summary>
    public class ScoreItemHighScore
    {
        /// <summary>
        /// The ScoreItem's id
        /// </summary>
        public ObjectId ScoreItemId { get; set; }

        /// <summary>
        /// Player id of highest scorer for the ScoreItem
        /// </summary>
        public ObjectId ScorerId { get; set; }

        /// <summary>
        /// Highest score for the ScoreItem
        /// </summary>
        public double ItemScore { get; set; }
    }
}