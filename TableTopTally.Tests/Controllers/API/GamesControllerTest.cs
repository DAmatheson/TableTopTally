using System;
using System.Net;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using TableTopTally.Controllers.API;
using TableTopTally.Helpers;
using TableTopTally.Models;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Tests.Controllers.API
{
    [TestFixture]
    public class GamesControllerTest
    {
        private static Mock<IGameService> GetGameServiceMock()
        {
            var gameServiceMock = new Mock<IGameService>();

            return gameServiceMock;
        }

        [Test(Description = "Test that GetAllGames returns all games")]
        public void GetAllGames_ShouldReturnAllGames()
        {
            // Arrange
            List<Game> games = new List<Game>
            {
                new Game { Name = "Pandemic", MinimumPlayers = 1, MaximumPlayers = 5},
                new Game { Name = "Caverna", MinimumPlayers = 3, MaximumPlayers = 6},
                new Game { Name = "Zombie Dice", MinimumPlayers = 2, MaximumPlayers = 10}
            };

            var gameServiceMock = GetGameServiceMock();
                
            gameServiceMock.Setup(g => g.GetGames()).Returns(games);

            var controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.GetAllGames() as OkNegotiatedContentResult<IEnumerable<Game>>;

            // Assert
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Content);
            Assert.AreEqual(games, result.Content);
            Assert.AreEqual(games.Count, result.Content.Count());
        }

        // Todo: Figure out what is returned if the db is empty
        [Test(Description = "Test that GetAllGames works with an empty DB")]
        public void GetAllGames_WorksWithEmptyDB()
        {
            // Arrange
            IEnumerable<Game> emptyDb = new List<Game>();

            var gameServiceMock = GetGameServiceMock();

            gameServiceMock.Setup(g => g.GetGames()).Returns(emptyDb);

            var controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.GetAllGames() as OkNegotiatedContentResult<IEnumerable<Game>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(emptyDb.Count(), result.Content.Count());
        }

        [Test(Description = "Test GetGameById with a valid ObjectId that does exist in the DB")]
        public void GetGameById_ValidObjectId()
        {
            // Arrange
            var game = new Game { Id = ObjectId.GenerateNewId() };

            var gameServiceMock = GetGameServiceMock();

            gameServiceMock.Setup(g => g.GetById(game.Id)).Returns(game);

            var controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.GetGameById(game.Id) as OkNegotiatedContentResult<Game>;

            // Assert
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Content);
            Assert.AreEqual(game, result.Content);
        }

        [Test(Description = "Test GetGameById with a valid ObjectId that doesn't exist in the DB")]
        public void GetGameById_NotInDb()
        {
            // Arrange
            ObjectId notInDbId = ObjectId.GenerateNewId();

            var gameServiceMock = GetGameServiceMock();

            gameServiceMock.Setup(g => g.GetById(notInDbId)).Returns<Game>(null);

            var controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.GetGameById(notInDbId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test(Description = "Test GetGameById with an invalid ObjectId")]
        public void GetGameById_InvalidObjectId()
        {
            // Arrange
            ObjectId invalidID = ObjectId.Empty;

            var gameServiceMock = GetGameServiceMock(); // No call should be made to IGameService

            gameServiceMock.Setup(g => g.GetById(invalidID)).Returns<Game>(null);

            var controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.GetGameById(invalidID) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test(Description = "Test GetGameByUrl with a valid Url")]
        public void GetGame_ValidUrl()
        {
            // Arrange
            var game = new Game { Url = "fake-url-8A801B" };

            var gameServiceMock = GetGameServiceMock();

            gameServiceMock.Setup(g => g.GetGameByUrl(game.Url)).Returns(game);

            var controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.GetGameByUrl(game.Url) as OkNegotiatedContentResult<Game>;

            // Assert
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Content);
            Assert.AreEqual(game, result.Content);
        }

        [Test(Description = "Test GetGameByUrl with an invalid Url")]
        public void GetGame_InvalidUrl()
        {
            // Arrange
            var gameServiceMock = GetGameServiceMock();

            gameServiceMock.Setup(g => g.GetGameByUrl("bad-url-8A0")).Returns((Game) null);

            var controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.GetGameByUrl("bad-url-8A0");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test(Description = "Test PostGame with a valid Game model")]
        public void PostGame_ValidModel()
        {
            // Arrange
            Game newGame = new Game
            {
                Name = "New Game",
                Id = ObjectId.Empty, // POSTed games don't provide an Id, so it would be ObjectId.Empty
                MinimumPlayers = 1,
                MaximumPlayers = 5,
            };

            var gameServiceMock = GetGameServiceMock();

            gameServiceMock.Setup(g => g.Create(newGame)).Returns(true);

            GamesController controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.PostGame(newGame) as CreatedAtRouteNegotiatedContentResult<Game>;

            // Assert

            // Result assertions
            Assert.IsNotNull(result);

            // 'Created at location' assertions
            Assert.AreEqual("DefaultApi", result.RouteName);
            Assert.AreEqual(newGame.Id, result.RouteValues["id"]);
            Assert.AreEqual(newGame.Name, result.Content.Name);
            Assert.AreEqual("Games", result.RouteValues["controller"]);

            // Value assertions
            Assert.AreEqual(newGame.Id, result.Content.Id);
            Assert.AreEqual(newGame.Name, result.Content.Name);
            Assert.AreEqual(newGame.Url, result.Content.Url);
            Assert.AreEqual(newGame.MinimumPlayers, result.Content.MinimumPlayers);
            Assert.AreEqual(newGame.MaximumPlayers, result.Content.MaximumPlayers);
            Assert.AreEqual(newGame, result.Content);
        }

        [Test(Description = "Test the PostGame with invalid model state")]
        public void PostGame_InvalidModel()
        {
            // Arrange
            Game newGame = new Game
            {
                Name = "New Game",
                MinimumPlayers = 1
            };

            var gameServiceMock = GetGameServiceMock();

            GamesController controller = new GamesController(gameServiceMock.Object);

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

        [Test(Description = "Test PutGame with a valid Game model that exists in the DB")]
        public void PutGame_ValidModel()
        {
            // Arrange
            Game updatedGame = new Game
            {
                Name = "Updated Pandemic",
                MinimumPlayers = 2,
                MaximumPlayers = 6
            };

            String oldUrl = "Pandemic".GenerateSlug(updatedGame.Id);

            updatedGame.Url = oldUrl;

            var gameServiceMock = GetGameServiceMock();

            gameServiceMock.Setup(g => g.Edit(updatedGame)).Returns(true);

            GamesController controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.PutGame(updatedGame.Id, updatedGame) as OkNegotiatedContentResult<Game>;

            // Assert

            // Result assertions
            Assert.IsNotNull(result);

            // Value assertions
            Assert.AreEqual(updatedGame.Id, result.Content.Id);
            Assert.AreEqual(updatedGame.Name, result.Content.Name);
            Assert.AreEqual(updatedGame.MinimumPlayers, result.Content.MinimumPlayers);
            Assert.AreEqual(updatedGame.MaximumPlayers, result.Content.MaximumPlayers);
            Assert.AreEqual(oldUrl, result.Content.Url);
            Assert.AreEqual(updatedGame, result.Content);
        }

        [Test(Description = "Test PutGame with a valid Game model that doesn't exist in the DB")]
        public void PutGame_NewGameCantUpdate()
        {
            // Arrange
            Game newGame = new Game
            {
                Name = "New Game Can't Be Updated",
                MinimumPlayers = 2,
                MaximumPlayers = 6
            };

            var gameServiceMock = GetGameServiceMock();

            gameServiceMock.Setup(g => g.Edit(newGame)).Returns(false);

            GamesController controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.PutGame(newGame.Id, newGame);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test(Description = "Test PutGame with ObjectId's that don't match")]
        public void PutGame_UnmatchedIds()
        {
            // Arrange
            Game updateGame = new Game();

            var gameServiceMock = GetGameServiceMock();

            GamesController controller = new GamesController(gameServiceMock.Object);

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
            Game newGame = new Game
            {
                Name = "Updating Game",
                MinimumPlayers = 1
            };

            var gameServiceMock = GetGameServiceMock();

            GamesController controller = new GamesController(gameServiceMock.Object);

            controller.ModelState.AddModelError("MaximumPlayers", "Invalid Max Players");

            // Act
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

        [Test(Description = "Test DeleteGame with a valid ObjectId that exists in the DB")]
        public void DeleteGame_ValidObjectId()
        {
            // Arrange
            ObjectId pandemicId = ObjectId.GenerateNewId();

            var gameServiceMock = GetGameServiceMock();

            gameServiceMock.Setup(g => g.Delete(pandemicId)).Returns(true);

            GamesController controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.DeleteGame(pandemicId) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test(Description = "Test DeleteGame with a valid ObjectId that doesn't exist in the DB")]
        public void DeleteGame_NonExistingObjectId()
        {
            // Arrange
            ObjectId notInDbId = ObjectId.GenerateNewId();

            var gameServiceMock = GetGameServiceMock();

            gameServiceMock.Setup(g => g.Delete(notInDbId)).Returns(false);

            GamesController controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.DeleteGame(notInDbId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test(Description = "Test DeleteGame with an invalid ObjectId")]
        public void DeleteGame_InvalidObjectId()
        {
            // Arrange
            ObjectId invalidId = ObjectId.Empty;

            var gameServiceMock = GetGameServiceMock(); // No call should be made to the IGameService

            GamesController controller = new GamesController(gameServiceMock.Object);

            // Act
            var result = controller.DeleteGame(invalidId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
