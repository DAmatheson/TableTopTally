/* Ranking.cs
 * 
 * Purpose: Class for a session ranking
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.31: Created
 */

using MongoDB.Bson;

namespace TableTopTally.Models
{
    public class Ranking
    {
        public ObjectId PlayerId { get; set; }
        public double Score { get; set; }
        public int Rank { get; set; }
    }
}