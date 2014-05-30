/* ScoreItem.cs
* 
* Purpose: A class for a game's scoring items
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/ 

using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class ScoreItem
    {
        /// <summary>
        /// The Scoring item's Id
        /// </summary>
        [ScaffoldColumn(false)]
        [BsonId]
        public ObjectId ScoreItemId { get; set; }

        /// <summary>
        /// The ScoreItems Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description of the ScoreItem
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}