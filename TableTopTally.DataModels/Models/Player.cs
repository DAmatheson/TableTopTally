/* Player.cs
 * 
 * Purpose: Class for players
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
    /// A player
    /// </summary>
    public class Player : MongoEntity
    {
        public Player() { }

        /// <summary>
        /// The Player's Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The GameGroup's the player is in
        /// </summary>
        public IList<ObjectId> Groups { get; set; }
    }
}