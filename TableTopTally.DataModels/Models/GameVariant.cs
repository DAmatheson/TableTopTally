/* GameVariant.cs
 * 
 * Purpose: A class for game variants / setups
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */

using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.DataModels.MongoDB.Entities;

namespace TableTopTally.DataModels.Models
{
    /// <summary>
    /// A Game variant
    /// </summary>
    public class GameVariant : MongoEntity
    {
        public GameVariant() { }

        /// <summary>
        /// The Game the variant is for
        /// </summary>
        public ObjectId GameId { get; set; }

        /// <summary>
        /// The GameGroup who created the variant
        /// </summary>
        public ObjectId GroupId { get; set; }

        /// <summary>
        /// Flag indicating whether to track historical high scores
        /// </summary>
        public bool TrackScores { get; set; }

        /// <summary>
        /// The GameVariant's Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Url for the GameVariant
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A collection of all  ScoreItems for the GameVariant
        /// </summary>
        public IList<ScoreItem> ScoreItems { get; set; }
    }
}