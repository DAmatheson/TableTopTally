using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using TableTopTally.Controllers.API;
using TableTopTally.Helpers;
using TableTopTally.Models;

namespace TableTopTally.Tests.Controllers.API
{
    [TestClass]
    public class GamesControllerTest
    {
        private readonly List<Game> games = new List<Game>()
        {
            new Game("Pandemic") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b2"), MinimumPlayers = 1, MaximumPlayers = 5},
            new Game("Caverna") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b3"), MinimumPlayers = 3, MaximumPlayers = 6},
            new Game("Zombie Dice") { Id = ObjectId.Parse("53e3a8ad6c46bc0c80ea13b4"), MinimumPlayers = 2, MaximumPlayers = 10}
        };

        [TestMethod]
        public void GetAllGames_ShouldReturnAllGames()
        {
            // Arrange
            var controller = new GamesController(games);

            // Act
            var result = controller.GetAllGames() as OkNegotiatedContentResult<List<Game>>;

            // Assert
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Content);
            Assert.AreEqual(games, result.Content);
        }

        [TestMethod]
        public void GetGame_ValidId()
        {
            // Arrange
            var controller = new GamesController(games);

            // Act
            var result = controller.GetGame(games[0].Id.ToString()) as OkNegotiatedContentResult<Game>;

            // Assert
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Content);
            Assert.AreEqual(games[0], result.Content);
        }

        [TestMethod]
        public void GetGame_InvalidId()
        {
            // Arrange
            var controller = new GamesController(games);

            // Act
            var result = controller.GetGame("");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostGame_ValidModel()
        {
            // Arrange
            Game newGame = new Game("New Game")
            {
                MinimumPlayers = 1,
                MaximumPlayers = 5
            };

            GamesController controller = new GamesController(games);

            // Act
            var result = controller.PostGame(newGame) as CreatedAtRouteNegotiatedContentResult<Game>;

            // Assert

            // Result assertions
            Assert.IsNotNull(result);

            // Route assertions
            Assert.AreEqual("DefaultApi", result.RouteName);
            Assert.AreEqual(newGame.Id, result.RouteValues["id"]);
            Assert.AreEqual(newGame.Name, result.Content.Name);
            Assert.AreEqual("Games", result.RouteValues["controller"]);

            // Value assertions
            Assert.AreEqual(newGame.Id, result.Content.Id);
            Assert.AreEqual("New Game", result.Content.Name);
            Assert.AreEqual("New Game".GenerateSlug(newGame.Id), result.Content.Url);
            Assert.AreEqual(newGame.MinimumPlayers, result.Content.MinimumPlayers);
            Assert.AreEqual(newGame.MaximumPlayers, result.Content.MaximumPlayers);
            Assert.AreEqual(games.FirstOrDefault(game => game.Id == newGame.Id), result.Content);
        }

        [TestMethod]
        public void PutGame_ValidModel()
        {
            // Arrange
            Game newGame = new Game("Updated Pandemic")
            {
                Id = games[0].Id,
                MinimumPlayers = 2,
                MaximumPlayers = 6
            };

            GamesController controller = new GamesController(games);

            // Act
            var result = controller.PutGame(games[0].Id.ToString(), newGame) as OkNegotiatedContentResult<Game>;

            // Assert

            // Result assertions
            Assert.IsNotNull(result);

            // Value assertions
            Assert.AreEqual(newGame.Id, result.Content.Id);
            Assert.AreEqual("Updated Pandemic", result.Content.Name);
            Assert.AreEqual(2, result.Content.MinimumPlayers);
            Assert.AreEqual(6, result.Content.MaximumPlayers);
            Assert.AreEqual("Updated Pandemic".GenerateSlug(games[0].Id), result.Content.Url);
            Assert.AreEqual(games.FirstOrDefault(game => game.Id == newGame.Id), result.Content);
        }

        [TestMethod]
        public void PutGame_InvalidId()
        {
            // Arrange
            Game updatedGame = new Game("Invalid ID")
            {
                Id = ObjectId.Empty,
                MinimumPlayers = 2,
                MaximumPlayers = 6
            };

            GamesController controller = new GamesController(games);

            // Act
            var result = controller.PutGame(updatedGame.Id.ToString(), updatedGame);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteGame_ValidId()
        {
            // Arrange
            ObjectId pandemicId = games[0].Id;

            GamesController controller = new GamesController(games);

            // Act
            var result = controller.DeleteGame(pandemicId.ToString()) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));

            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void DeleteGame_InvalidId()
        {
            // Arrange
            String invalidId = ObjectId.Empty.ToString();

            GamesController controller = new GamesController(games);

            // Act
            var result = controller.DeleteGame(invalidId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
