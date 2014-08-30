using MongoDB.Bson;
using NUnit.Framework;
using System.Web.Http.Results;
using TableTopTally.Controllers.API;
using TableTopTally.Models;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Tests.Controllers.API
{
    [TestFixture]
    public class GamesControllerTest
    {
        //private readonly List<Game> games = new List<Game>()
        //{
        //    new Game("Pandemic") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b2"), MinimumPlayers = 1, MaximumPlayers = 5},
        //    new Game("Caverna") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b3"), MinimumPlayers = 3, MaximumPlayers = 6},
        //    new Game("Zombie Dice") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b4"), MinimumPlayers = 2, MaximumPlayers = 10}
        //};

        //[TestMethod]
        //public void GetAllGames_ShouldReturnAllGames()
        //{
        //    // Arrange
        //    var controller = new GamesController(games);

        //    // Act
        //    var result = controller.GetAllGames() as OkNegotiatedContentResult<List<Game>>;

        //    // Assert
        //    Assert.IsNotNull(result);

        //    Assert.IsNotNull(result.Content);
        //    Assert.AreEqual(games, result.Content);
        //    Assert.AreEqual(games.Count, result.Content.Count);
        //}

        //[TestMethod]
        //public void GetGame_ValidObjectId()
        //{
        //    // Arrange
        //    var controller = new GamesController(games);

        //    // Act
        //    var result = controller.GetGameById(games[0].Id) as OkNegotiatedContentResult<Game>;

        //    // Assert
        //    Assert.IsNotNull(result);

        //    Assert.IsNotNull(result.Content);
        //    Assert.AreEqual(games[0], result.Content);
        //}

        //[TestMethod]
        //public void GetGame_InvalidObjectId()
        //{
        //    // Arrange
        //    var controller = new GamesController(games);

        //    // Act
        //    var result = controller.GetGameById(ObjectId.Empty) as NotFoundResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}

        //[TestMethod]
        //public void GetGame_ValidUrl()
        //{
        //    // Arrange
        //    var controller = new GamesController(games);

        //    // Act
        //    var result = controller.GetGameByUrl(games[0].Url) as OkNegotiatedContentResult<Game>;

        //    // Assert
        //    Assert.IsNotNull(result);

        //    Assert.IsNotNull(result.Content);
        //    Assert.AreEqual(games[0], result.Content);
        //}

        //[TestMethod]
        //public void GetGame_InvalidUrl()
        //{
        //    // Arrange
        //    var controller = new GamesController(games);

        //    // Act
        //    var result = controller.GetGameByUrl("blah-00000");

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}

        //[TestMethod]
        //public void PostGame_ValidModel()
        //{
        //    // Arrange
        //    Game newGame = new Game("New Game")
        //    {
        //        MinimumPlayers = 1,
        //        MaximumPlayers = 5
        //    };

        //    GamesController controller = new GamesController(games);

        //    // Act
        //    var result = controller.PostGame(newGame) as CreatedAtRouteNegotiatedContentResult<Game>;

        //    // Assert

        //    // Result assertions
        //    Assert.IsNotNull(result);

        //    // Route assertions
        //    Assert.AreEqual("DefaultApi", result.RouteName);
        //    Assert.AreEqual(newGame.Id, result.RouteValues["id"]);
        //    Assert.AreEqual(newGame.Name, result.Content.Name);
        //    Assert.AreEqual("Games", result.RouteValues["controller"]);

        //    // Value assertions
        //    Assert.AreEqual(newGame.Id, result.Content.Id);
        //    Assert.AreEqual("New Game", result.Content.Name);
        //    Assert.AreEqual("New Game".GenerateSlug(newGame.Id), result.Content.Url);
        //    Assert.AreEqual(newGame.MinimumPlayers, result.Content.MinimumPlayers);
        //    Assert.AreEqual(newGame.MaximumPlayers, result.Content.MaximumPlayers);
        //    Assert.AreEqual(newGame, result.Content);
        //}

        [Test(Description = "Test the PostGame with invalid model state")]
        public void PostGame_InvalidModel()
        {
            // Arrange
            Game newGame = new Game("New Game")
            {
                MinimumPlayers = 1
            };

            GamesController controller = new GamesController(new GameService());

            // Act
            controller.ModelState.AddModelError("MaximumPlayers", "Invalid Max Players");
            var result = controller.PostGame(newGame) as InvalidModelStateResult;

            // Assert

            // Result assertions
            Assert.IsNotNull(result);

            // Value assertions
            Assert.IsFalse(result.ModelState.IsValid);
            Assert.IsFalse(result.ModelState.IsValidField("MaximumPlayers"));
            Assert.IsTrue(result.ModelState.ContainsKey("MaximumPlayers"));
            Assert.AreEqual("Invalid Max Players", result.ModelState["MaximumPlayers"].Errors[0].ErrorMessage);
        }

        //[TestMethod]
        //public void PutGame_ValidModel()
        //{
        //    // Arrange
        //    Game newGame = new Game("Updated Pandemic")
        //    {
        //        Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b2"),
        //        MinimumPlayers = 2,
        //        MaximumPlayers = 6
        //    };

        //    newGame.Url = newGame.Name.GenerateSlug(newGame.Id);

        //    GamesController controller = new GamesController(games);

        //    // Act
        //    var result = controller.PutGame(newGame.Id, newGame) as OkNegotiatedContentResult<Game>;

        //    // Assert

        //    // Result assertions
        //    Assert.IsNotNull(result);

        //    // Value assertions
        //    Assert.AreEqual(newGame.Id, result.Content.Id);
        //    Assert.AreEqual("Updated Pandemic", result.Content.Name);
        //    Assert.AreEqual(2, result.Content.MinimumPlayers);
        //    Assert.AreEqual(6, result.Content.MaximumPlayers);
        //    Assert.AreEqual(newGame.Url, result.Content.Url);
        //    Assert.AreEqual(games.FirstOrDefault(g => g.Id == newGame.Id), result.Content);
        //}

        //[TestMethod]
        //public void PutGame_NewGameCantUpdate()
        //{
        //    // Arrange
        //    Game updatedGame = new Game("New Game Can't Be Updated")
        //    {
        //        Id = ObjectId.GenerateNewId(),
        //        MinimumPlayers = 2,
        //        MaximumPlayers = 6
        //    };

        //    GamesController controller = new GamesController(games);

        //    // Act
        //    var result = controller.PutGame(updatedGame.Id, updatedGame);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}

        [Test(Description = "Test PutGame with ObjectId's that don't match")]
        public void PutGame_UnmatchedIds()
        {
            // Arrange
            Game updateGame = new Game("New Game - Url Id doesn't match");

            GamesController controller = new GamesController(new GameService());

            // Act
            var result = controller.PutGame(ObjectId.GenerateNewId(), updateGame);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test(Description = "Test PutGame with invalid model state")]
        public void PutGame_InvalidModel()
        {
            // Arrange
            Game newGame = new Game("New Game")
            {
                MinimumPlayers = 1
            };

            GamesController controller = new GamesController(new GameService());

            // Act
            controller.ModelState.AddModelError("MaximumPlayers", "Invalid Max Players");
            var result = controller.PutGame(newGame.Id, newGame) as InvalidModelStateResult;

            // Assert

            // Result assertions
            Assert.IsNotNull(result);

            // Value assertions
            Assert.IsFalse(result.ModelState.IsValid);
            Assert.IsFalse(result.ModelState.IsValidField("MaximumPlayers"));
            Assert.IsTrue(result.ModelState.ContainsKey("MaximumPlayers"));
            Assert.AreEqual("Invalid Max Players", result.ModelState["MaximumPlayers"].Errors[0].ErrorMessage);
        }

        //[TestMethod]
        //public void DeleteGame_ValidObjectId()
        //{
        //    // Arrange
        //    ObjectId pandemicId = games[0].Id;

        //    GamesController controller = new GamesController(games);

        //    // Act
        //    var result = controller.DeleteGame(pandemicId) as StatusCodeResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(StatusCodeResult));

        //    Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        //}

        //[TestMethod]
        //public void DeleteGame_InvalidObjectId()
        //{
        //    // Arrange
        //    ObjectId invalidId = ObjectId.Empty;

        //    GamesController controller = new GamesController(games);

        //    // Act
        //    var result = controller.DeleteGame(invalidId);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}
    }
}
