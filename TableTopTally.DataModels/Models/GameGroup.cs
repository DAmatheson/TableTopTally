/* GameGroup.cs
 * Purpose: Represents a all the things about and related to a gaming group
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
    /// A gaming group with members, list of owned games, and list of game variants
    /// </summary>
    public class GameGroup : MongoEntity
    {
        /// <summary>
        /// PlayerId of the creator of the group
        /// </summary>
        public ObjectId CreatorId { get; set; }

        /// <summary>
        /// List of the players in the group
        /// </summary>
        public IList<Player> Members { get; set; }

        /// <summary>
        /// List of games the group owns. 
        /// </summary>
        /// <remarks>Shown when starting a session</remarks>
        public IList<ObjectId> GameIds { get; set; }

        /// <summary>
        /// Ids of the variant's the group created/can use
        /// </summary>
        // Unsure: Maybe IDictionary<ObjectId, IList<ObjectId>> VariantsByGame ?
        public IList<ObjectId> VariantIds { get; set; }
    }
}