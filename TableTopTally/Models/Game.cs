﻿/* Game.cs
 * 
 * Purpose: A class for games
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MongoDB.Bson;
using TableTopTally.Annotations;
using TableTopTally.Attributes;
using TableTopTally.Helpers;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Models
{
    [Bind(Include = "Name, MinimumPlayers, MaximumPlayers")]
    public class Game : MongoEntity
    {
        [UsedImplicitly]
        public Game() { }

        /// <summary>
        /// Initializes a new instance of the Game class
        /// </summary>
        /// <param name="name">The name of the game</param>
        public Game(string name)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            Url = name.GenerateSlug(Id);
        }

        /// <summary>
        /// The Game's Name
        /// </summary>
        [Required(ErrorMessage = "Game name is required.", AllowEmptyStrings = false)]
        [StringLength(
            70,
            ErrorMessage = "The name must be between 1 and 70 characters",
            MinimumLength = 1)
        ]
        public string Name { get; set; }

        /// <summary>
        /// The Url for the Game
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The Minimum number of players for the game
        /// </summary>
        [Required(ErrorMessage = "Minimum Players number is required.")]
        [Range(1, 99, ErrorMessage = "Minimum Players must be between 1 and 99.")]
        public int MinimumPlayers { get; set; }

        /// <summary>
        /// The Maximum number of players for the game
        /// </summary>
        [Required(ErrorMessage = "Maximum Players number is required.")]
        [Range(1, 99, ErrorMessage =  "Maximum Players must be between 1 and 99.")]
        [CompareValues("MinimumPlayers", CompareValues.GreatThanOrEqualTo,
            ErrorMessage = "Minimum Players must be less than or equal to Maximum Players.")]
        public int MaximumPlayers { get; set; }
    }
}