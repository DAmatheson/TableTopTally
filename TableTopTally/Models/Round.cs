/* Round.cs
 * 
 * Purpose: Class for session rounds
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
        public Round() { }

        /// <summary>
        /// Initializes a new instance of the Round class, optionally auto-initializing properties
        /// </summary>
        /// <param name="initialize">bool indicating whether to auto-initialize properties</param>
        public Round(bool initialize)
        {
            if (initialize)
            {
                Id = ObjectId.GenerateNewId();
                Scores = new List<PlayerScore>();
            }
        }

        /// <summary>
        /// The Round's Id
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// A collection of all the PlayerScore's for the round
        /// </summary>
        public IList<PlayerScore> Scores { get; set; }
    }
}