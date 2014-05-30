/* PlaySession.cs
* 
* Purpose: Class for play sessions
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/ 

using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class PlaySession
    {
        /// <summary>
        /// The Session's Id
        /// </summary>
        [BsonId]
        public ObjectId SessionId { get; set; }

        /// <summary>
        /// A collection of all the Round's of the Session
        /// </summary>
        public IEnumerable<Round> Rounds { get; set; }
    }
}