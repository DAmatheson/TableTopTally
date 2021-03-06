﻿/* PlayerScore.cs
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

namespace TableTopTally.DataModels.Models
{
    /// <summary>
    /// A player's scores from a round
    /// </summary>
    public class PlayerScore
    {
        public PlayerScore() { }

        /// <summary>
        /// The Player's Id
        /// </summary>
        public ObjectId PlayerId { get; set; }

        /// <summary>
        /// The key is the ObjectId of the scoring item, and the value is the player's score for that item
        /// </summary>
        public IDictionary<ObjectId, double> ItemScores { get; set; }

        /// <summary>
        /// The Player's score for the round
        /// </summary>
        [BsonIgnore]
        public double ScoreTotal
        {
            get
            {
                double total = 0f;

                if (ItemScores != null && ItemScores.Any())
                {
                    total = ItemScores.Sum(scoreItem => scoreItem.Value);
                }
                return total;
            }
        }
    }
}