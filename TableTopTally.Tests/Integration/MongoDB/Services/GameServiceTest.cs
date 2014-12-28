using MongoDB.Bson;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TableTopTally.Helpers;
using TableTopTally.Models;
using TableTopTally.MongoDB;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Tests.MongoDB.Services
{
    [TestFixture]
    public class GameServiceTest
    {
        private static readonly List<Game> mockGames = new List<Game>
            {
                new Game { Name = "Game1", MinimumPlayers = 1, MaximumPlayers = 5 },
                new Game { Name = "Game2", MinimumPlayers = 2, MaximumPlayers = 6 },
                new Game { Name = "Game3", MinimumPlayers = 3, MaximumPlayers = 7 }
            };

        /// <summary>
        /// Clear out the games collection in the test database before each test
        /// </summary>
        [SetUp]
        public void ClearGamesCollection()
        {
            //Clear db
            var collection = MongoHelper.GetTableTopCollection<Game>();

            collection.Drop();
        }

        /// <summary>
        /// Fill the Games collection in the test database with mock data for testing
        /// </summary>
        private void FillGamesCollection()
        {
            var service = new GameService();

            foreach (var game in mockGames)
            {
                game.Url = game.Name.URLFriendly(game.Id);

                service.Create(game);
            }
        }

        [Test(Description = "Test GetGames with an empty collection")]
        public void GetGames_EmptyCollection()
        {
            // Arrange
            var service = new GameService();

            // Act
            IEnumerable<Game> games = service.GetGames();

            // Assert
            Assert.IsNotNull(games);
            Assert.That(games.Count(), Is.EqualTo(0));
        }

        [Test(Description = "Test GetGames after populating the collection via FillGamesCollection")]
        public void GetGames_FilledCollection()
        {
            // Arrange
            FillGamesCollection();

            var service = new GameService();

            // Act
            IEnumerable<Game> games = service.GetGames();

            // Assert
            Assert.IsNotNull(games);
            Assert.IsInstanceOf<IEnumerable<Game>>(games);

            var gamesList = games.ToList();

            Assert.That(gamesList.Count, Is.EqualTo(mockGames.Count));
            Assert.That(gamesList[0].Name, Is.EqualTo(mockGames[0].Name));
            Assert.That(gamesList[0].MinimumPlayers, Is.EqualTo(mockGames[0].MinimumPlayers));
            Assert.That(gamesList[0].MaximumPlayers, Is.EqualTo(mockGames[0].MaximumPlayers));
            Assert.That(gamesList[0].Url, Is.EqualTo(mockGames[0].Url));
        }

        [Test(Description = "Test GetById with an Id that exists in the collection")]
        public void GetById_IdInCollection()
        {
            // Arrange
            FillGamesCollection();

            var service = new GameService();

            // Act
            Game game = service.GetById(mockGames[0].Id);

            // Assert
            Assert.IsNotNull(game);
            Assert.That(game.Name, Is.EqualTo(mockGames[0].Name));
            Assert.That(game.MinimumPlayers, Is.EqualTo(mockGames[0].MinimumPlayers));
            Assert.That(game.MaximumPlayers, Is.EqualTo(mockGames[0].MaximumPlayers));
            Assert.That(game.Id, Is.EqualTo(mockGames[0].Id));
        }

        [Test(Description = "Test GetById with an Id that doesn't exist in the collection")]
        public void GetById_IdNotInCollection()
        {
            // Arrange
            FillGamesCollection();

            var service = new GameService();

            // Act
            Game game = service.GetById(ObjectId.GenerateNewId());

            // Assert
            Assert.IsNull(game);
        }

        [Test(Description = "Test Delete with an Id that exists in the collection")]
        public void Delete_IdInCollection()
        {
            // Arrange
            FillGamesCollection();

            var service = new GameService();

            // Act
            bool deleted = service.Delete(mockGames[0].Id);

            // Assert
            Assert.IsTrue(deleted);
        }

        [Test(Description = "Test Delete with an Id that doesn't exist in the collection")]
        public void Delete_IdNotInCollection()
        {
            // Arrange
            var service = new GameService();

            // Act
            bool deleted = service.Delete(ObjectId.GenerateNewId());

            // Assert
            Assert.IsFalse(deleted);
        }

        [Test(Description = "Test Create with an Id that doesn't exist in the collection")]
        public void Create_ValidId()
        {
            // Arrange
            FillGamesCollection();

            Game game = new Game
            {
                Id = ObjectId.GenerateNewId(),
                Name = "A New Game",
                MinimumPlayers = 1,
                MaximumPlayers = 2
            };

            game.Url = game.Name.URLFriendly(game.Id);

            var service = new GameService();

            // Act
            bool created = service.Create(game);

            // Assert
            Assert.IsTrue(created);
        }

        [Test(Description = "Test Create with an Id that exists in the collection")]
        public void Create_InvalidId()
        {
            // Arrange
            FillGamesCollection();

            Game game = new Game
            {
                Id = mockGames[0].Id,
                Name = "A invalid new game",
                MinimumPlayers = 1,
                MaximumPlayers = 2
            };

            game.Url = game.Name.URLFriendly(mockGames[0].Id);

            var service = new GameService();

            // Act
            bool created = service.Create(game);

            // Assert
            Assert.IsFalse(created);
        }

        [Test(Description = "Test Edit game with an Id that exists in the collection")]
        public void Edit_Valid()
        {
            // Arrange
            FillGamesCollection();

            Game updatedGame = new Game
            {
                Id = mockGames[0].Id,
                Name = mockGames[0].Name + " Edit",
                MinimumPlayers = 3,
                MaximumPlayers = 5,
                Url = mockGames[0].Url
            };

            var service = new GameService();

            // Act
            bool success = service.Edit(updatedGame);

            // Assert
            Assert.IsTrue(success);
        }

        [Test(Description = "Test Edit with an Id that doesn't exist in the collection")]
        public void Edit_InvalidId()
        {
            // Arrange
            Game updatedGame = new Game
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Invalid Id For Update",
                MinimumPlayers = 2,
                MaximumPlayers = 3
            };

            updatedGame.Url = updatedGame.Name.URLFriendly(updatedGame.Id);

            var service = new GameService();

            // Act
            bool success = service.Edit(updatedGame);

            // Assert
            Assert.IsFalse(success);
        }

        [Test(Description = "Test GetGameByUrl with a Url that exists in the collection")]
        public void GetGameByUrl_ValidUrl()
        {
            // Arrange
            FillGamesCollection();

            var service = new GameService();

            // Act
            Game game = service.GetGameByUrl(mockGames[0].Url);

            // Assert
            Assert.IsNotNull(game);
            Assert.That(game.Id, Is.EqualTo(mockGames[0].Id));
            Assert.That(game.Name, Is.EqualTo(mockGames[0].Name));
            Assert.That(game.MinimumPlayers, Is.EqualTo(mockGames[0].MinimumPlayers));
            Assert.That(game.MaximumPlayers, Is.EqualTo(mockGames[0].MaximumPlayers));
            Assert.That(game.Url, Is.EqualTo(mockGames[0].Url));
        }

        [Test(Description = "Test GetGameByUrl with a Url that doesn't exist in the collection")]
        public void GetGameByUrl_InvalidUrl()
        {
            // Arrange
            var service = new GameService();

            // Act
            Game game = service.GetGameByUrl("a-fake-url-0d9s8d");

            // Assert
            Assert.IsNull(game);
        }
    }
}
