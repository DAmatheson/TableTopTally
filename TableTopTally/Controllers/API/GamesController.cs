/* GamesController.cs
 * Purpose: API controller for Game objects
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.07: Created
*/

using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TableTopTally.Annotations;
using TableTopTally.Helpers;
using TableTopTally.Models;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Controllers.API
{
    [RoutePrefix("api/games")]
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
        public GamesController()
        {
            // Required because games is recreated at time an action in the controller is called
            games[0].Url = games[0].Name.GenerateSlug(games[0].Id);
            games[1].Url = games[1].Name.GenerateSlug(games[1].Id);
            games[2].Url = games[2].Name.GenerateSlug(games[2].Id);
        }

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
        /// <param name="id">ObjectId of the game</param>
        /// <returns>The Game</returns>
        [Route("{id:objectId}")]
        [HttpGet]
        public IHttpActionResult GetGameById(ObjectId id)
        {
            ObjectId parsedId = id;
            Game game = null;

            //ObjectId.TryParse(id, out parsedId);

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

        // GET: api/Games/5
        /// <summary>
        /// Get a game by its ObjectId
        /// </summary>
        /// <param name="url">Url string of the game</param>
        /// <returns>The Game</returns>
        [Route("{url}")]
        [HttpGet]
        public IHttpActionResult GetGameByUrl(string url)
        {
            //Game game = gameService.GetById(parsedId);
            Game game = games.FirstOrDefault((g) => g.Url == url);

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
            // Remove game.Id model error because POST actions don't include an Id for game
            if (ModelState.ContainsKey("game.Id"))
            {
                ModelState["game.Id"].Errors.Clear();
            }

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
        [Route("{id:objectId}")]
        [HttpPut]
        public IHttpActionResult PutGame([FromUri]ObjectId id, Game game)
        {
            IHttpActionResult result;

            if (ModelState.IsValid)
            {
                Game gameToUpdate = null; // Note: No DB use only

                // Unsure: Does making the id and game.Id match really prevent exploitation?
                if (id == game.Id)
                {
                    gameToUpdate = games.FirstOrDefault(g => g.Id == id); // Note: No DB use only

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

                if (gameToUpdate != null) // Note: No DB use only
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
        [Route("{id:objectId}")]
        [HttpDelete]
        public IHttpActionResult DeleteGame([FromUri]ObjectId id)
        {
            bool removed = false;

            if (id != ObjectId.Empty)
            {
                //removed = gameService.Delete(parsedId);

                var game = games.FirstOrDefault(g => g.Id == id);
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