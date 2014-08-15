/* GamesController.cs
 * 
 * Purpose: API controller for Game objects
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.07: Created
*/ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MongoDB.Bson;
using TableTopTally.Helpers;
using TableTopTally.Models;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Controllers.API
{
    public class GamesController : ApiController
    {
        //private readonly GameService gameService;

        // Mock data for easier angular production / general testing
        private List<Game> games = new List<Game>()
        {
            new Game("Pandemic") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b2"), MinimumPlayers = 1, MaximumPlayers = 5},
            new Game("Caverna") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b3"), MinimumPlayers = 3, MaximumPlayers = 6},
            new Game("Zombie Dice") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b4"), MinimumPlayers = 2, MaximumPlayers = 10}
        };

        //public GamesController()
        //{
        //gameService = new GameService();
        //}

        // GET: api/Games
        /// <summary>
        /// Gets all games
        /// </summary>
        /// <returns>List of all the games</returns>
        [HttpGet]
        public IHttpActionResult Get()
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
        public IHttpActionResult Get(string id)
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
        public IHttpActionResult Post(Game game)
        {
            //var success = gameService.Create(game);

            game.Id = ObjectId.GenerateNewId();
            game.Url = game.Name.GenerateSlug(game.Id);

            games.Add(game);

            var location = Url.Link("DefaultApi", new { controller = "Games", id = game.Id });

            return Created(location, game);
        }

        // PUT: api/Games/5
        /// <summary>
        /// Updates the Game with the matching id
        /// </summary>
        /// <param name="id">The id of the game to update</param>
        /// <param name="value">The new values to update</param>
        /// <returns>Status code</returns>
        [HttpPut]
        public IHttpActionResult Put(string id, [FromBody] string value)
        {
            ObjectId parsedId;

            ObjectId.TryParse(id, out parsedId);

            if (parsedId != ObjectId.Empty)
            {
                var game = games.FirstOrDefault(g => g.Id == parsedId);
                if (game != null)
                {
                    game.Name = value;
                }

                //var game = new Game { id = parsedId, values = value };
                //var success = gameService.Edit(game);
            }

            return Ok();
        }

        // DELETE: api/Games/5
        /// <summary>
        /// Deletes the game with the matching id
        /// </summary>
        /// <param name="id">The id of the game to delete</param>
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            ObjectId parsedId;
            bool removed = false;

            ObjectId.TryParse(id, out parsedId);

            if (parsedId != ObjectId.Empty)
            {
                //removed = gameService.Delete(id);

                var game = games.FirstOrDefault(g => g.Id == parsedId);
                removed = games.Remove(game);
            }

            if (!removed)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}