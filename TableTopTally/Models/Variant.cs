using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class Variant
    {
        [ScaffoldColumn(false)]
        [BsonId]
        public ObjectId VariantId { get; set; }

        /// <summary>
        /// The Variant's Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The Url for the Variant
        /// </summary>
        [ScaffoldColumn(false)]
        public string Url { get; set; }

        /// <summary>
        /// A collection of all  ScoreItems for the Variant
        /// </summary>
        public IEnumerable<ScoreItem> ScoreItems { get; set; }
    }
}