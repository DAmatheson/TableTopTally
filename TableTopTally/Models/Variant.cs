/* Variant.cs
 * 
 * Purpose: A class for game variants / setups
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */ 

using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.Helpers;
using TableTopTally.MongoDB;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Models
{
    public class Variant : MongoEntity
    {
        public Variant() { }

        /// <summary>
        /// Initializes a new instance of the Variant class
        /// </summary>
        /// <param name="name">The name of the variant</param>
        public Variant(string name)
        {
            Id = ObjectId.GenerateNewId();

            // Todo: Consider not initializing and changing prop to IEnumerable
            ScoreItems = new List<ScoreItem>();

            Name = name;
            Url = name.GenerateSlug();
        }

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