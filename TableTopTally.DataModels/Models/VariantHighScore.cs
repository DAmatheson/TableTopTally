/* VariantHighScore.cs
 * Purpose: A class for representing a Game Variant's various high scores
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.18: Created
 */

using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.DataModels.MongoDB.Entities;

namespace TableTopTally.DataModels.Models
{
    /// <summary>
    /// A game Variant's various high scores
    /// </summary>
    public class VariantHighScore : MongoEntity
    {
        /// <summary>
        /// GroupId of the GameGroup the VariantHighScore is for
        /// </summary>
        public ObjectId GroupId { get; set; }

        /// <summary>
        /// The VariantId the VariantHighScore is for
        /// </summary>
        public ObjectId VariantId { get; set; }

        /// <summary>
        /// The number of players in the session
        /// </summary>
        public int NumberOfPlayers { get; set; }

        /// <summary>
        /// Player id of highest scorer for the session
        /// </summary>
        public ObjectId ScorerId { get; set; }

        /// <summary>
        /// Highest overall session score
        /// </summary>
        public double SessionScore { get; set; }

        /// <summary>
        /// List of all the round highscores for the variant
        /// </summary>
        public List<RoundHighScore> RoundHighScores { get; set; }
    }
}