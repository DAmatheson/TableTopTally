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
        ///     Create a game in the database
        /// </summary>
        /// <param name="game">Game object to be created</param>
        /// <returns>Returns a bool representing if the creation completed successfully</returns>
        public override bool Create(Game game)
        {
            // Unsure: Should setting game.Variants be done in controller, game constructor, or this service?

            if (!game.Variants.Any())
            {
                game.Variants = new List<Variant>();
            }

            return !collection.Insert(game).HasLastErrorMessage;
        }

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
                    Set("Url", game.Url)).
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
        ///     Gets a game by its Url value and returns the result without its variants
        /// </summary>
        /// <param name="gameUrl">Url value of the game to find</param>
        /// <returns>A Game object with an empty Variants property</returns>
        public Game GetVariantlessGameByUrl(string gameUrl)
        {
            // Todo: Better name for this method. It will be used for editing a game's information
            return collection.Find(Query.EQ("Url", gameUrl)).
                SetFields(Fields.Exclude("Variants")).
                Single();
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

            game.Variants = game.Variants.OrderBy(c => c.Name).ToList();

            return game;
        }
    }
}