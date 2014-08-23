/* GameService.cs
* 
* Purpose: A class with methods for CRUDing Game documents in MongoDB
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/

using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Builders;
using TableTopTally.Models;

namespace TableTopTally.MongoDB.Services
{
    /// <summary>
    ///     Provides methods for CRUDing games to a mongoDB database
    /// </summary>
    public class GameService : MongoService<Game>, IGameService 
    {
        /// <summary>
        ///     Updates the game in the database
        /// </summary>
        /// <param name="game">Game representing the game to be update</param>
        /// <returns>A bool representing if the edit completed successfully</returns>
        public bool Edit(Game game)
        {
            return !collection.Update(
                Query.EQ("_id", game.Id),
                Update.Set("Name", game.Name).
                    Set("Url", game.Url).
                    Set("MinimumPlayers", game.MinimumPlayers).
                    Set("MaximumPlayers", game.MaximumPlayers)).
                HasLastErrorMessage;
        }

        /// <summary>
        ///     Gets all of the games in the database sorted by Name and without Variants
        /// </summary>
        /// <returns>An IEnumerable of type Game sorted by Name</returns>
        public IEnumerable<Game> GetGames()
        {
            // Unsure: Sort in mongo or C#? Also: Should I .ToList()? Doing so will take care of disposing the cursor

            return collection.FindAll().
                SetFields(Fields.Exclude("Variants")).
                SetSortOrder(SortBy.Ascending("Name"));
        }

        /// <summary>
        ///     Gets a game by its Url value and returns the result as a Game with variants ordered by 
        ///     their Names
        /// </summary>
        /// <param name="gameUrl">Url value of the game to find</param>
        /// <returns>A Game object with variants ordered by their Names</returns>
        public Game GetGameByUrl(string gameUrl)
        {
            // UNSURE: Sort in mongo or C#?

            Game game = collection.Find(Query.EQ("Url", gameUrl)).
                Single();

            return game;
        }
    }
}