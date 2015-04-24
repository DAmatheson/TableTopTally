/* ScoreItem.cs
 * 
 * Purpose: A class for a game's scoring items
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */

using TableTopTally.DataModels.MongoDB.Entities;

namespace TableTopTally.DataModels.Models
{
    /// <summary>
    /// A single scoring item from a Game
    /// </summary>
    public class ScoreItem : MongoEntity
    {
        public ScoreItem() { }

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