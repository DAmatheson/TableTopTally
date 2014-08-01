using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MongoDB.Bson;
using TableTopTally.Models;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Controllers.API
{
    public class GamesController : ApiController
    {
        //private readonly GameService gameService;

        private List<Game> games = new List<Game>()
        {
            new Game("Pandemic"),
            new Game("Caverna"),
            new Game("Zombie Dice")
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
            return Json(games);
        }

        // GET: api/Games/5
        /// <summary>
        /// Get game by its ObjectId
        /// </summary>
        /// <param name="id">ObjectId string of the game</param>
        /// <returns>Json of the Game</returns>
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            //var game = gameService.GetById(id);

            var game = games.FirstOrDefault((g) => g.Id == ObjectId.Parse(id));

            if (game == null)
            {
                return NotFound();
            }

            return Json(game);
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
            //gameService.Create(game);

            game.Id = ObjectId.GenerateNewId();

            games.Add(game);

            var location = Url.Link("DefaultApi", new { controller = "Games", id = game.Id });

            return Created(location, game);
        }

        // PUT: api/Games/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Games/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
