/* Player.cs
* 
* Purpose: Class for players
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/ 

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class Player
    {
        /// <summary>
        /// The Player's Id
        /// </summary>
        [BsonId]
        public ObjectId PlayerId { get; set; }

        /// <summary>
        /// The Player's Name
        /// </summary>
        public string Name { get; set; }
    }
}