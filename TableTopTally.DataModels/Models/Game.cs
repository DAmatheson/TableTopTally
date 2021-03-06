﻿/* Game.cs
 * 
 * Purpose: A class for games
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TableTopTally.DataModels.MongoDB.Entities;
using TableTopTally.DataModels.Validation;

namespace TableTopTally.DataModels.Models
{
    //[Bind(Include = "Name, MinimumPlayers, MaximumPlayers")]
    public class Game : MongoEntity
    {
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
        /// Note: This is only set in the DB when first created so that it doesn't change due to edits
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The Minimum number of players for the game
        /// </summary>
        [DisplayName("Minimum Players")]
        [Required(ErrorMessage = "Minimum Players number is required.")]
        [Range(1, 99, ErrorMessage = "Minimum Players must be between 1 and 99.")]
        public int MinimumPlayers { get; set; }

        /// <summary>
        /// The Maximum number of players for the game
        /// </summary>
        [DisplayName("Maximum Players")]
        [Required(ErrorMessage = "Maximum Players number is required.")]
        [Range(1, 99, ErrorMessage =  "Maximum Players must be between 1 and 99.")]
        [CompareValues("MinimumPlayers", ComparisonCriteria.GreatThanOrEqualTo,
            ErrorMessage = "Minimum Players must be less than or equal to Maximum Players.")]
        public int MaximumPlayers { get; set; }
    }
}