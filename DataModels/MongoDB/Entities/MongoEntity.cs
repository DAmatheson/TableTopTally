/* MongoEntity.cs
 * Purpose: Generic entity for MongoDB
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
*/ 

using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TableTopTally.Helpers;

namespace TableTopTally.MongoDB.Entities
{
    /// <summary>
    /// Generic MongoDB entity
    /// </summary>
    public abstract class MongoEntity : IMongoEntity
    {
        /// <summary>
        /// The MongoEntity's Id 
        /// </summary>
        [BsonId]
        [CustomValidation(typeof (ObjectIdModelValidator), "IsValid")]
        public ObjectId Id { get; set; }
    }
}