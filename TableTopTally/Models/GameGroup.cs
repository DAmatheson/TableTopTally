using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Models
{
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