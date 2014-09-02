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


        [SetUp] // Clear out the collection before each test
        public void ClearGamesCollection()
        {
            //Clear db
            var collection = MongoHelper.GetTableTopCollection<Game>();

            collection.Drop();
        }

        private void FillGamesCollection()
        {
            //Fill db

            var service = new GameService();

            foreach (var game in mockGames)
            {
                game.Url = game.Name.GenerateSlug(game.Id);

                service.Create(game);
            }
        }

        [Test(Description = "Test GetById with an empty collection")]
        public void GetGames_EmptyCollection()
        {
            // Arrange
            var service = new GameService();

            // Act
            IEnumerable<Game> games = service.GetGames();

            // Assert
            Assert.IsNotNull(games);
            Assert.AreEqual(0, games.Count());
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

            Assert.AreEqual(mockGames.Count, gamesList.Count);
            Assert.AreEqual(mockGames[0].Name, gamesList[0].Name);
            Assert.AreEqual(mockGames[0].MinimumPlayers, gamesList[0].MinimumPlayers);
            Assert.AreEqual(mockGames[0].MaximumPlayers, gamesList[0].MaximumPlayers);
            Assert.AreEqual(mockGames[0].Url, gamesList[0].Url);
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
            Assert.AreEqual(mockGames[0].Name, game.Name);
            Assert.AreEqual(mockGames[0].MinimumPlayers, game.MinimumPlayers);
            Assert.AreEqual(mockGames[0].MaximumPlayers, game.MaximumPlayers);
            Assert.AreEqual(mockGames[0].Id, game.Id);
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
    }
}
