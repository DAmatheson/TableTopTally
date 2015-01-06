/* GamesController.cs
 * Purpose: API controller for Game objects
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.07: Created
*/

using MongoDB.Bson;
using System.Net;
using System.Web.Http;
using TableTopTally.Helpers;
using TableTopTally.Models;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Controllers.API
{
    [RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        // GET: api/Games
        /// <summary>
        /// Gets all games
        /// </summary>
        /// <returns>List of all the games</returns>
        [HttpGet]
        public IHttpActionResult GetAllGames()
        {
            var games = gameService.GetGames();

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
            Game game;

            if (id != ObjectId.Empty)
            {
                game = gameService.FindById(id);
            }
            else
            {
                return BadRequest("The id specified isn't valid");
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
            Game game = gameService.FindByUrl(url);

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
                game.Url = game.Name.URLFriendly(game.Id);

                var success = gameService.Add(game);

                if (success)
                {
                    return CreatedAtRoute("DefaultApi", new { controller = "Games", id = game.Id  }, game);
                }

                return InternalServerError(); // Todo: Better response to failed mongo creation
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

            // Unsure: Does making the id and game.Id match really prevent exploitation?
            if (ModelState.IsValid && id == game.Id)
            {
                var success = gameService.Edit(game);

                if (success)
                {
                    result = Ok(game);
                }
                else
                {
                    result = NotFound();
                }
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    result = BadRequest(ModelState);
                }
                else
                {
                    result = BadRequest("URL Id and game Id don't match.");
                }
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
                removed = gameService.Remove(id);
            }

            if (!removed)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}