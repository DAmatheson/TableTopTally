/* IMongoEntity.cs
 * Purpose: Interface for a MongoDB Entity
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */

using MongoDB.Bson;

namespace TableTopTally.DataModels.MongoDB.Entities
{
    /// <summary>
    /// MongoDB Entity Interface
    /// </summary>
    public interface IMongoEntity
    {
        /// <summary>
        /// The ObjectId of the entity
        /// </summary>
        ObjectId Id { get; set; }
    }
}