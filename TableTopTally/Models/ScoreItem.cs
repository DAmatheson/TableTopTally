/* ScoreItem.cs
 * 
 * Purpose: A class for a game's scoring items
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */ 

using MongoDB.Bson;
using TableTopTally.Annotations;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Models
{
    /// <summary>
    /// A single scoring item from a Game
    /// </summary>
    public class ScoreItem : MongoEntity
    {
        [UsedImplicitly]
        public ScoreItem() { }

        /// <summary>
        /// Initializes a new instance of the ScoreItem class
        /// </summary>
        /// <param name="name">The name of the score item</param>
        /// <param name="description">A description for the score item</param>
        public ScoreItem(string name, string description)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            Description = description;
        }

        /// <summary>
        /// The ScoreItems Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the ScoreItem
        /// </summary>
        public string Description { get; set; }
    }
}