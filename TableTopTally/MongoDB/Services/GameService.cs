/* GameService.cs
* 
* Purpose: A class with methods for CRUDing Game documents in MongoDB
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/

using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Builders;
using TableTopTally.Models;

namespace TableTopTally.MongoDB.Services
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
        public bool Edit(Game game)
        {
            return collection.Update(
                Query.EQ("_id", game.Id),
                Update.Set("Name", game.Name).
                    Set("Url", game.Url).
                    Set("MinimumPlayers", game.MinimumPlayers).
                    Set("MaximumPlayers", game.MaximumPlayers)).
                UpdatedExisting;
        }

        /// <summary>
        /// Gets all of the games in the database sorted by Name
        /// </summary>
        /// <returns>An IEnumerable of type Game sorted by Name</returns>
        public IEnumerable<Game> GetGames()
        {
            // Unsure: Sort in mongo or C#? Also: Should I .ToList()? Doing so will take care of disposing the cursor

            return collection.FindAll().SetSortOrder(SortBy.Ascending("Name"));
                //SetFields(Fields.Exclude("Variants")).
        }

        /// <summary>
        /// Gets a game by its Url value and returns the result
        /// </summary>
        /// <param name="gameUrl">Url value of the game to find</param>
        /// <returns>The Game object or null if a gameUrl is unmatched</returns>
        public Game FindByUrl(string gameUrl)
        {
            Game game;

            try
            {
                game = collection.Find(Query.EQ("Url", gameUrl)).Single();
            }
            catch (InvalidOperationException)
            {
                game = null; // Return null when the result returns zero or more than on result
            }

            return game;
        }
    }
}