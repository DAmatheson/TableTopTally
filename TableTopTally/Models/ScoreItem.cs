using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class ScoreItem
    {
        [ScaffoldColumn(false)]
        [BsonId]
        public ObjectId RuleId { get; set; }

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