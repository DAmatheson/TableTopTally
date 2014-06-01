﻿/* ScoreItem.cs
 * 
 * Purpose: A class for a game's scoring items
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */ 

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TableTopTally.Models
{
    public class ScoreItem
    {
        public ScoreItem() { }

        /// <summary>
        /// Initializes a new instance of the ScoreItem class
        /// </summary>
        /// <param name="name">The name of the score item</param>
        /// <param name="description">A description for the score item</param>
        public ScoreItem(string name, string description)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            Description = description;
        }

        /// <summary>
        /// The Scoring item's Id
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// The ScoreItems Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the ScoreItem
        /// </summary>
        public string Description { get; set; }
    }
}