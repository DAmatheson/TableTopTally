/* RankScores.cs
 * Purpose: Class with methods for ranking a Sessions Rounds Scores
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.09: Created
 */ 

using System.Collections.Generic;
using System.Linq;
using TableTopTally.Models;

namespace TableTopTally.Helpers
{
    /// <summary>
    /// Contains methods for ranking sessions' scores
    /// </summary>
    public static class RankScores
    {
        /// <summary>
        ///     Rank top 3 players by descending score
        /// </summary>
        /// <param name="unranked">The Rankings to rank</param>
        /// <returns>IEnumerable for the top 3 rankings</returns>
        public static IEnumerable<Ranking> Descending(IEnumerable<Ranking> unranked)
        {
            var grouped = unranked.GroupBy(r => r.Score);
            var ordered = grouped.OrderByDescending(g => g.Key);

            int totalRank = 1;

            foreach (var group in ordered)
            {
                int rank = totalRank;

                foreach (var ranking in group)
                {
                    ranking.Rank = rank;

                    yield return ranking;

                    if (++totalRank > 3)
                    {
                        yield break;
                    }
                }
            }
        }

        /// <summary>
        ///     Ranks the top 3 players by descending score
        /// </summary>
        /// <param name="session">The session to be ranked</param>
        /// <returns>A collection of the top 3 rankings</returns>
        public static IEnumerable<Ranking> RankSession(PlaySession session)
        {
            List<Ranking> ranks = (from player in session.Players
                                   let total =
                                       session.Rounds.Sum(
                                           round =>
                                               round.Scores.Where(
                                                   playerScore => playerScore.PlayerId == player.Id).
                                               Sum(playerScore => playerScore.ScoreTotal))
                                   select new Ranking { PlayerId = player.Id, Score = total }).ToList();

            return Descending(ranks);
        }
    }
}