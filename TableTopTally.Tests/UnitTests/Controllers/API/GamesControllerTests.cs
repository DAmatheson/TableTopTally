using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using TableTopTally.Controllers.API;
using TableTopTally.Models;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Tests.UnitTests.Controllers.API
{
    [TestFixture]
    public class GamesControllerTests
    {
        private const string STRING_OBJECT_ID = "53e3a8ad6c46bc0c80ea13b2";

        private static Mock<IGameService> GetGameServiceFake()
        {
            var getGameServiceFake = new Mock<IGameService>();

            return getGameServiceFake;
        }

        [Test(Description = "Test that GetGames returns all games")]
        public void GetGames_WhenCalled_ReturnsAllGames()
        {
            List<Game> games = new List<Game>
            {
                new Game { Name = "TestGame" },
                new Game { Name = "TestGame2" },
            };

            var gameServiceStub = GetGameServiceFake();   
            gameServiceStub.Setup(g => g.GetGames()).Returns(games);

            var controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.GetAllGames() as OkNegotiatedContentResult<IEnumerable<Game>>;

            Assert.IsNotNull(result);
            Assert.That(result.Content, Is.EqualTo(games));
        }

        // Todo: Figure out what is returned if the db is empty
        [Test(Description = "Test that GetGames works with an empty DB")]
        public void GetGames_EmptyDB_ReturnsEmptyList()
        {
            IEnumerable<Game> emptyDb = new List<Game>();

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.GetGames()).Returns(emptyDb);

            var controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.GetAllGames() as OkNegotiatedContentResult<IEnumerable<Game>>;

            Assert.IsNotNull(result);
            Assert.That(result.Content.Count(), Is.EqualTo(emptyDb.Count()));
        }

        [Test(Description = "Test GetGameById with a valid ObjectId that does exist in the DB")]
        public void GetGameById_IdInDb_ReturnsMatchingGame()
        {
            var game = new Game { Id = new ObjectId(STRING_OBJECT_ID) };

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.FindById(game.Id)).Returns(game);

            var controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.GetGameById(game.Id) as OkNegotiatedContentResult<Game>;

            Assert.IsNotNull(result);
            Assert.That(result.Content, Is.EqualTo(game));
        }

        [Test(Description = "Test GetGameById with a valid ObjectId that doesn't exist in the DB")]
        public void GetGameById_IdNotInDb_ReturnsNotFoundResult()
        {
            ObjectId notInDbId = new ObjectId(STRING_OBJECT_ID);

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.FindById(notInDbId)).Returns<Game>(null);

            var controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.GetGameById(notInDbId) as NotFoundResult;

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        // The API model binder for ObjectId only passes in non-empty ObjectIds
        [Test(Description = "Test GetGameById with an invalid ObjectId")]
        public void GetGameById_InvalidObjectId_ReturnsBadRequest()
        {
            var gameServiceStub = GetGameServiceFake();

            var controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.GetGameById(ObjectId.Empty) as BadRequestErrorMessageResult;

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test(Description = "Test GetGameByUrl with a valid Url")]
        public void GetGame_ValidUrl_ReturnsMatchingGame()
        {
            var game = new Game { Url = "fake-url-8A801B" };

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.GetGameByUrl(game.Url)).Returns(game);

            var controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.GetGameByUrl(game.Url) as OkNegotiatedContentResult<Game>;

            Assert.IsNotNull(result);
            Assert.That(result.Content, Is.EqualTo(game));
        }

        [Test(Description = "Test GetGameByUrl with an invalid Url")]
        public void GetGame_InvalidUrl_ReturnsNotFound()
        {
            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.GetGameByUrl("bad-url-8A0")).Returns((Game) null);

            var controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.GetGameByUrl("bad-url-8A0") as NotFoundResult;

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test(Description = "Test PostGame with a valid Game model")]
        public void PostGame_ValidData_CreatesNewEntry()
        {
            Game newGame = new Game
            {
                Name = "New Game",
                Id = ObjectId.Empty, // POSTed games don't provide an Id, so it would be ObjectId.Empty
                MinimumPlayers = 1,
                MaximumPlayers = 2,
            };

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.Add(newGame)).Returns(true);

            GamesController controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.PostGame(newGame) as CreatedAtRouteNegotiatedContentResult<Game>;

            Assert.IsNotNull(result);
            Assert.That(result.Content.Id, Is.EqualTo(newGame.Id));
            Assert.That(result.Content.Name, Is.EqualTo(newGame.Name));
            Assert.That(result.Content.Url, Is.EqualTo(newGame.Url));
            Assert.That(result.Content.MinimumPlayers, Is.EqualTo(newGame.MinimumPlayers));
            Assert.That(result.Content.MaximumPlayers, Is.EqualTo(newGame.MaximumPlayers));
            Assert.That(result.Content, Is.EqualTo(newGame));
        }

        [Test(Description = "Test PostGame with valid data but a model error for empty Id")]
        public void PostGame_EmptyObjectIdError_StillCreatesNewEntry()
        {
            Game newGame = new Game
            {
                Name = "New Game",
                Id = ObjectId.Empty, // POSTed games don't provide an Id, so it would be ObjectId.Empty
                MinimumPlayers = 1,
                MaximumPlayers = 2,
            };

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.Add(newGame)).Returns(true);

            GamesController controller = new GamesController(gameServiceStub.Object);
            controller.ModelState.AddModelError("game.Id", "Id is required.");

            // Act
            var result = controller.PostGame(newGame) as CreatedAtRouteNegotiatedContentResult<Game>;

            Assert.IsNotNull(result);
            Assert.That(result.Content, Is.EqualTo(newGame));
        }

        [Test(Description = "Test PostGame with a valid Game model to see if it returns the location")]
        public void PostGame_ValidData_ReturnsLocationOfEntry()
        {
            Game newGame = new Game
            {
                Name = "New Game",
                Id = ObjectId.Empty,
                MinimumPlayers = 1,
                MaximumPlayers = 5,
            };

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.Add(newGame)).Returns(true);

            GamesController controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.PostGame(newGame) as CreatedAtRouteNegotiatedContentResult<Game>;

            Assert.IsNotNull(result);
            Assert.That(result.RouteName, Is.EqualTo("DefaultApi"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Games"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(newGame.Id));
        }

        [Test(Description = "Test PostGame with a valid Game model to see if it returns the created game")]
        public void PostGame_ValidData_ReturnsCreatedGame()
        {
            Game newGame = new Game
            {
                Name = "New Game",
                Id = ObjectId.Empty,
                MinimumPlayers = 1,
                MaximumPlayers = 5,
            };

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.Add(newGame)).Returns(true);

            GamesController controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.PostGame(newGame) as CreatedAtRouteNegotiatedContentResult<Game>;

            Assert.IsNotNull(result);
            Assert.That(result.Content, Is.EqualTo(newGame));
        }

        [Test(Description = "Test PostGame with invalid model state")]
        public void PostGame_InvalidModel_ReturnsInvalidModelState()
        {
            Game newGame = new Game
            {
                Name = "New Game",
                MinimumPlayers = 1
            };

            var gameServiceStub = GetGameServiceFake();

            GamesController controller = new GamesController(gameServiceStub.Object);
            controller.ModelState.AddModelError("MaximumPlayers", "Invalid Max Players");

            // Act
            var result = controller.PostGame(newGame) as InvalidModelStateResult;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.ModelState.IsValid);
            Assert.IsFalse(result.ModelState.IsValidField("MaximumPlayers"));
        }

        [Test(Description = "Test PostGame with the creation failing")]
        public void PostGame_CreationFails_ReturnsInternalServerError()
        {
            Game newGame = new Game()
            {
                Name = "New Game"
            };

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.Add(newGame)).Returns(false);

            GamesController controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.PostGame(newGame) as InternalServerErrorResult;

            Assert.IsInstanceOf<InternalServerErrorResult>(result);
        }

        [Test(Description = "Test PutGame with a valid Game model that exists in the DB")]
        public void PutGame_UpdateSucceeds_ReturnsUpdatedGame()
        {
            Game updatedGame = new Game
            {
                Name = "Updated Pandemic",
                MinimumPlayers = 2,
                MaximumPlayers = 6
            };

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.Edit(updatedGame)).Returns(true);

            GamesController controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.PutGame(updatedGame.Id, updatedGame) as OkNegotiatedContentResult<Game>;

            Assert.IsNotNull(result);
            Assert.That(result.Content.Id, Is.EqualTo(updatedGame.Id));
            Assert.That(result.Content.Name, Is.EqualTo(updatedGame.Name));
            Assert.That(result.Content.MinimumPlayers, Is.EqualTo(updatedGame.MinimumPlayers));
            Assert.That(result.Content.MaximumPlayers, Is.EqualTo(updatedGame.MaximumPlayers));
            Assert.That(result.Content, Is.EqualTo(updatedGame));
        }

        [Test(Description = "Test PutGame with a valid Game model that doesn't exist in the DB")]
        public void PutGame_UpdateFails_ReturnsNotFound()
        {
            Game newGame = new Game
            {
                Name = "New Game Can't Be Updated",
                MinimumPlayers = 2,
                MaximumPlayers = 6
            };

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.Edit(newGame)).Returns(false);

            GamesController controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.PutGame(newGame.Id, newGame);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test(Description = "Test PutGame with ObjectId's that don't match")]
        public void PutGame_UnmatchedIds_ReturnsBadRequest()
        {
            Game updateGame = new Game();

            var gameServiceStub = GetGameServiceFake();

            GamesController controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.PutGame(new ObjectId(STRING_OBJECT_ID), updateGame);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test(Description = "Test PutGame with invalid model state")]
        public void PutGame_InvalidModel_ReturnsInvalidModelState()
        {
            // Arrange
            Game newGame = new Game
            {
                Name = "Updating Game",
                MinimumPlayers = 1
            };

            var gameServiceStub = GetGameServiceFake();

            GamesController controller = new GamesController(gameServiceStub.Object);
            controller.ModelState.AddModelError("MaximumPlayers", "Invalid Max Players");

            // Act
            var result = controller.PutGame(newGame.Id, newGame) as InvalidModelStateResult;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.ModelState.IsValid);
            Assert.IsFalse(result.ModelState.IsValidField("MaximumPlayers"));
        }

        [Test(Description = "Test DeleteGame with a valid ObjectId that exists in the DB")]
        public void DeleteGame_DeleteSucceeds_ReturnsNoContentCode()
        {
            ObjectId validId = new ObjectId(STRING_OBJECT_ID);

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.Remove(validId)).Returns(true);

            GamesController controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.DeleteGame(validId) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test(Description = "Test DeleteGame with a valid ObjectId that doesn't exist in the DB")]
        public void DeleteGame_DeleteFails_ReturnsNotFound()
        {
            ObjectId notInDbId = new ObjectId(STRING_OBJECT_ID);

            var gameServiceStub = GetGameServiceFake();
            gameServiceStub.Setup(g => g.Remove(notInDbId)).Returns(false);

            GamesController controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.DeleteGame(notInDbId) as NotFoundResult;

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        // The API model binder for ObjectId only passes in non-empty ObjectIds
        [Test(Description = "Test DeleteGame with an invalid ObjectId")]
        public void DeleteGame_InvalidObjectId_ReturnsNotFound()
        {
            ObjectId invalidId = ObjectId.Empty;

            var gameServiceStub = GetGameServiceFake();

            GamesController controller = new GamesController(gameServiceStub.Object);

            // Act
            var result = controller.DeleteGame(invalidId) as NotFoundResult;

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
