/* Ranking.cs
 * 
 * Purpose: Class for session rankings
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.31: Created
 *      Drew Matheson, 2014.06.1: Made setters automatically move previous player down a rank
 */ 

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class Ranking
    {
        private ObjectId firstPlaceId;

        /// <summary>
        /// The ObjectId for the player who came in first
        /// </summary>
        public ObjectId FirstPlaceId
        {
            get
            {
                return firstPlaceId;
            }

            set
            {
                if (firstPlaceId != ObjectId.Empty)
                {
                    SecondPlaceId = firstPlaceId;
                }

                firstPlaceId = value;
            }
        }

        private ObjectId secondPlaceId;

        /// <summary>
        /// The ObjectId for the player who came in second
        /// </summary>
        [BsonIgnoreIfDefault]
        public ObjectId SecondPlaceId 
        {
            get
            {
                return secondPlaceId;
            }
            set
            {
                if (secondPlaceId != ObjectId.Empty)
                {
                    ThirdPlaceId = secondPlaceId;
                }

                secondPlaceId = value;
            } 
        }

        /// <summary>
        /// The ObjectId for the player who came in third
        /// </summary>
        [BsonIgnoreIfDefault]
        public ObjectId ThirdPlaceId { get; set; }
    }
}