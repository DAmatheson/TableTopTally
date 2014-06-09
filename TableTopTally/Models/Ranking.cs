/* Ranking.cs
 * 
 * Purpose: Class for session rankings
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.31: Created
 *      Drew Matheson, 2014.06.1: Made setters automatically move previous player down a rank
 */

using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;
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