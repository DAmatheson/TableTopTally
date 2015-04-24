/* Variant.cs
 * 
 * Purpose: A class for game variants / setups
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */ 

using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Models
{
    /// <summary>
    /// A Game variant
    /// </summary>
    public class Variant : MongoEntity
    {
        public Variant() { }

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
        /// The Variant's Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Url for the Variant
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A collection of all  ScoreItems for the Variant
        /// </summary>
        public IList<ScoreItem> ScoreItems { get; set; }
    }
}