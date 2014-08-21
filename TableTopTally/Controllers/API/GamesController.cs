/* GamesController.cs
 * Purpose: API controller for Game objects
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.07: Created
*/

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using MongoDB.Bson;
using TableTopTally.Annotations;
using TableTopTally.Helpers;
using TableTopTally.Models;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Controllers.API
{
    public class GamesController : ApiController
    {
        private readonly IGameService gameService;

        // Mock data for easier angular production / general testing
        private List<Game> games = new List<Game>()
        {
            new Game("Pandemic") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b2"), MinimumPlayers = 1, MaximumPlayers = 5},
            new Game("Caverna") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b3"), MinimumPlayers = 3, MaximumPlayers = 6},
            new Game("Zombie Dice") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b4"), MinimumPlayers = 2, MaximumPlayers = 10}
        };

        [UsedImplicitly]
        public GamesController() { }

        public GamesController(List<Game> gameService)
        {
            //this.gameService = gameService;

            games = gameService;
        }

        // GET: api/Games
        /// <summary>
        /// Gets all games
        /// </summary>
        /// <returns>List of all the games</returns>
        [HttpGet]
        public IHttpActionResult GetAllGames()
        {
            //return gameService.GetGames();
            return Ok(games);
        }

        // GET: api/Games/5
        /// <summary>
        /// Get a game by its ObjectId
        /// </summary>
        /// <param name="id">ObjectId string of the game</param>
        /// <returns>The Game</returns>
        [HttpGet]
        public IHttpActionResult GetGame(string id)
        {
            ObjectId parsedId;
            Game game = null;

            ObjectId.TryParse(id, out parsedId);

            if (parsedId != ObjectId.Empty)
            {
                //game = gameService.GetById(parsedId);
                game = games.FirstOrDefault((g) => g.Id == parsedId);
            }

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // POST: api/Games
        /// <summary>
        /// Adds a game
        /// </summary>
        /// <param name="game">Game to add</param>
        /// <returns>Status code and location of added resource</returns>
        [HttpPost]
        public IHttpActionResult PostGame(Game game)
        {
            if (ModelState.IsValid)
            {
                game.Id = ObjectId.GenerateNewId();
                game.Url = game.Name.GenerateSlug(game.Id);

                games.Add(game);
                //var success = gameService.Create(game);

                return CreatedAtRoute("DefaultApi", new { controller = "Games", id = game.Id  }, game);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Games/5
        /// <summary>
        /// Updates the Game with the matching id
        /// </summary>
        /// <param name="id">The id of the game to update</param>
        /// <param name="game">The game properties to update</param>
        /// <returns>Status code and if successful, the new game</returns>
        [HttpPut]
        public IHttpActionResult PutGame(string id, Game game)
        {
            IHttpActionResult result;

            if (ModelState.IsValid)
            {
                Game gameToUpdate = null;

                ObjectId parsedId;

                ObjectId.TryParse(id, out parsedId);

                // Unsure: Does making the id and game.Id match really prevent exploitation?
                if (parsedId != ObjectId.Empty && parsedId == game.Id)
                {
                    gameToUpdate = games.FirstOrDefault(g => g.Id == parsedId);

                    // game.Id = parsedId;

                    //var success = gameService.Edit(game);

                    //if (success)
                    //{
                    //    result = Ok(game);
                    //}
                    //else
                    //{
                    //    result = NotFound();
                    //}
                }

                if (gameToUpdate != null)
                {
                    gameToUpdate.Name = game.Name;
                    gameToUpdate.MinimumPlayers = game.MinimumPlayers;
                    gameToUpdate.MaximumPlayers = game.MaximumPlayers;
                    gameToUpdate.Url = game.Name.GenerateSlug(gameToUpdate.Id);

                    result = Ok(gameToUpdate);
                }
                else
                {
                    result = NotFound();
                }
            }
            else
            {
                result = BadRequest(ModelState);
            }

            return result;
        }

        // DELETE: api/Games/5
        /// <summary>
        /// Deletes the game with the matching id
        /// </summary>
        /// <param name="id">The id of the game to delete</param>
        [HttpDelete]
        public IHttpActionResult DeleteGame(string id)
        {
            ObjectId parsedId;
            bool removed = false;

            ObjectId.TryParse(id, out parsedId);

            if (parsedId != ObjectId.Empty)
            {
                //removed = gameService.Delete(parsedId);

                var game = games.FirstOrDefault(g => g.Id == parsedId);
                removed = games.Remove(game);
            }

            if (!removed)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}