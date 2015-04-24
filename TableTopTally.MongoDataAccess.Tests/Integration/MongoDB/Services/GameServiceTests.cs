using System.Collections.Generic;
using System.Linq;
using TableTopTally.MongoDataAccess.Services;
using TableTopTally.DataModels.Models;
using MongoDB.Bson;
using NUnit.Framework;

namespace TableTopTally.Tests.Integration.MongoDB.Services
{
    [TestFixture]
    public class GameServiceTests : BaseMongoServiceTests<GameService, Game>
    {
        private readonly List<Game> fakeGames = new List<Game>
            {
                new Game { Name = "Game1", MinimumPlayers = 1, MaximumPlayers = 5 },
                new Game { Name = "Game2", MinimumPlayers = 2, MaximumPlayers = 6 },
                new Game { Name = "Game3", MinimumPlayers = 3, MaximumPlayers = 7 }
            };

        protected override GameService GetService()
        {
            return new GameService();
        }

        protected override Game CreateEntity(string idString)
        {
            Game game = new Game
            {
                Id = new ObjectId(idString),
                Name = "Game",
                MinimumPlayers = 1,
                MaximumPlayers = 2,
                Url = "url"
            };

            return game;
        }

        private void FillGamesCollection(GameService service, IEnumerable<Game> games)
        {
            foreach (Game game in games)
            {
                AddEntityToCollection(game, service);
            }
        }

        [Test]
        public void GetGames_EmptyCollection_ReturnsEmptyEnumerable()
        {
            GameService service = GetService();

            // Act
            IEnumerable<Game> games = service.GetGames();

            Assert.IsNotNull(games);
            Assert.That(games, Is.Empty);
        }

        [Test]
        public void GetGames_FilledCollection_ReturnsSameGames()
        {
            GameService service = GetService();
            FillGamesCollection(service, fakeGames);

            // Act
            IEnumerable<Game> retrievedGames = service.GetGames();

            Assert.IsNotNull(retrievedGames);

            List<Game> gamesList = retrievedGames.ToList();

            Assert.That(gamesList.Count, Is.EqualTo(fakeGames.Count));
            Assert.That(gamesList[0].Id, Is.EqualTo(fakeGames[0].Id));
            Assert.That(gamesList[1].Id, Is.EqualTo(fakeGames[1].Id));
            Assert.That(gamesList[2].Id, Is.EqualTo(fakeGames[2].Id));
        }

        [Test]
        public override void FindById_IdInCollection_ReturnsMatchingEntity()
        {
            Game entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameService service = GetService();
            AddEntityToCollection(entity, service);

            // Act
            Game game = service.FindById(entity.Id);

            Assert.IsNotNull(game);
            Assert.That(game.Name, Is.EqualTo(entity.Name));
            Assert.That(game.MinimumPlayers, Is.EqualTo(entity.MinimumPlayers));
            Assert.That(game.MaximumPlayers, Is.EqualTo(entity.MaximumPlayers));
            Assert.That(game.Id, Is.EqualTo(entity.Id));
        }

        [Test]
        public void Edit_IdInDb_ReturnsTrue()
        {
            Game entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameService service = GetService();
            AddEntityToCollection(entity, service);

            entity.Name = "Game Edited";

            // Act
            bool success = service.Edit(entity);

            Assert.IsTrue(success);
        }

        [Test]
        public void Edit_IdNotInDb_ReturnsFalse()
        {
            Game updatedGame = CreateEntity(VALID_STRING_OBJECT_ID);
            GameService service = GetService();

            // Act
            bool success = service.Edit(updatedGame);

            Assert.IsFalse(success);
        }

        [Test]
        public void FindByUrl_ValidUrl_ReturnsMatchingGame()
        {
            Game entity = CreateEntity(VALID_STRING_OBJECT_ID);

            GameService service = GetService();

            AddEntityToCollection(entity, service);

            // Act
            Game retrieved = service.FindByUrl(entity.Url);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.Name, Is.EqualTo(entity.Name));
            Assert.That(retrieved.MinimumPlayers, Is.EqualTo(entity.MinimumPlayers));
            Assert.That(retrieved.MaximumPlayers, Is.EqualTo(entity.MaximumPlayers));
            Assert.That(retrieved.Url, Is.EqualTo(entity.Url));
        }

        [Test]
        public void FindByUrl_NonExistantUrl_ReturnsNull()
        {
            GameService service = GetService();

            // Act
            Game retrieved = service.FindByUrl("a-non-existant-url");

            Assert.IsNull(retrieved);
        }
    }
}
