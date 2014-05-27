using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class PlayerScore
    {
        [BsonId]
        public ObjectId PlayerScoreId { get; set; }

        /// <summary>
        /// The Player's ID
        /// </summary>
        public ObjectId PlayerId { get; set; }

        /// <summary>
        /// The Player's score for the round
        /// </summary>
        public float Score { get; set; }
    }
}