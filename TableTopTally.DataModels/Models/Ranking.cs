/* Ranking.cs
 * 
 * Purpose: Class for a generic ranking
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.31: Created
 */

using MongoDB.Bson;

namespace TableTopTally.DataModels.Models
{
    /// <summary>
    /// A generic ranking for a single player
    /// </summary>
    public class Ranking
    {
        public ObjectId PlayerId { get; set; }
        public double Score { get; set; }
        public int Rank { get; set; }
    }
}