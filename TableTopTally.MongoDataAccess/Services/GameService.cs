/* GameService.cs
* 
* Purpose: A class with methods for CRUDing Game documents in MongoDB
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/

using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TableTopTally.DataModels.Models;

namespace TableTopTally.MongoDataAccess.Services
{
    /// <summary>
    /// Provides methods for CRUDing games to a mongoDB database
    /// </summary>
    public class GameService : MongoService<Game>, IGameService 
    {
        /// <summary>
        /// Updates the game in the database
        /// </summary>
        /// <param name="game">Game representing the game to be update</param>
        /// <returns>A bool representing if the edit completed successfully</returns>
        public async Task<bool> EditAsync(Game game)
        {
            var result = await collection.UpdateOneAsync(g => g.Id == game.Id,
                Builders<Game>.Update.
                    Set(g => g.Name, game.Name).
                    Set(g => g.MinimumPlayers, game.MinimumPlayers).
                    Set(g => g.MaximumPlayers, game.MaximumPlayers));

            return result.MatchedCount == 1;
        }

        /// <summary>
        /// Gets all of the games in the database sorted by Name
        /// </summary>
        /// <returns>An IEnumerable of type Game sorted by Name</returns>
        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            return await collection.
                Find(EmptyFilter).
                Sort(Builders<Game>.Sort.Ascending(g => g.Name)).
                ToListAsync();
        }

        /// <summary>
        /// Gets a game by its Url value and returns the result.  If no match is found, null is returned
        /// </summary>
        /// <param name="gameUrl">Url value of the game to find</param>
        /// <returns>The Game object or null if a gameUrl is unmatched</returns>
        public async Task<Game> FindByUrlAsync(string gameUrl)
        {
            return await collection.
                Find(Builders<Game>.Filter.Eq(g => g.Url, gameUrl)).
                FirstOrDefaultAsync();
        }
    }
}