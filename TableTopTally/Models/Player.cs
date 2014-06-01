﻿/* Player.cs
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
        public Player() { }

        /// <summary>
        /// Initializes a new instance of the Player class
        /// </summary>
        /// <param name="name">The name of the player</param>
        public Player(string name)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
        }

        /// <summary>
        /// The Player's Id
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// The Player's Name
        /// </summary>
        public string Name { get; set; }
    }
}