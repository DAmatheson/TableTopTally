/* Round.cs
 * 
 * Purpose: Class for session rounds
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */ 

using System.Collections.Generic;
using TableTopTally.Annotations;

namespace TableTopTally.Models
{
    /// <summary>
    /// A Session round
    /// </summary>
    public class Round
    {
        [UsedImplicitly]
        public Round() { }

        /// <summary>
        /// Initializes a new instance of the Round class, optionally auto-initializing properties
        /// </summary>
        /// <param name="initialize">bool indicating whether to auto-initialize properties</param>
        public Round(bool initialize)
        {
            if (initialize)
            {
                Scores = new List<PlayerScore>();
            }
        }

        /// <summary>
        /// The round number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// A collection of all the PlayerScore's for the round
        /// </summary>
        public IList<PlayerScore> Scores { get; set; }
    }
}