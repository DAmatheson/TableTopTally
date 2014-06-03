/* Game.cs
 * 
 * Purpose: A class for games
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */ 

using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.Helpers;
using TableTopTally.MongoDB;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Models
{
    public class Game : MongoEntity
    {
        public Game() { }

        /// <summary>
        /// Initializes a new instance of the Game class
        /// </summary>
        /// <param name="name">The name of the game</param>
        public Game(string name)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            Url = name.GenerateSlug();
        }

        /// <summary>
        /// The Game's Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Url for the Game
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A collection of all of the Game's Variants
        /// </summary>
        public IList<Variant> Variants { get; set; }
    }
}