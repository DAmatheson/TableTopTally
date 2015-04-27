using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.DataModels.Models;
using TableTopTally.MongoDataAccess.Services;

namespace MongoDataAccess.Tests.Integration.Services
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

        private async Task FillGamesCollection(GameService service, IEnumerable<Game> games)
        {
            foreach (Game game in games)
            {
                await AddEntityToCollection(game, service);
            }
        }

        [Test]
        public async Task GetGames_EmptyCollection_ReturnsEmptyEnumerable()
        {
            GameService service = GetService();

            // Act
            IEnumerable<Game> games = await service.GetGamesAsync();

            Assert.IsNotNull(games);
            Assert.That(games, Is.Empty);
        }

        [Test]
        public async Task GetGames_FilledCollection_ReturnsSameGames()
        {
            GameService service = GetService();
            await FillGamesCollection(service, fakeGames);

            // Act
            IEnumerable<Game> retrievedGames = await service.GetGamesAsync();

            Assert.IsNotNull(retrievedGames);

            List<Game> gamesList = retrievedGames.ToList();

            Assert.That(gamesList.Count, Is.EqualTo(fakeGames.Count));
            Assert.That(gamesList[0].Id, Is.EqualTo(fakeGames[0].Id));
            Assert.That(gamesList[1].Id, Is.EqualTo(fakeGames[1].Id));
            Assert.That(gamesList[2].Id, Is.EqualTo(fakeGames[2].Id));
        }

        [Test]
        public override async Task FindById_IdInCollection_ReturnsMatchingEntity()
        {
            Game entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameService service = GetService();
            await AddEntityToCollection(entity, service);

            // Act
            Game game = await service.FindByIdAsync(entity.Id);

            Assert.IsNotNull(game);
            Assert.That(game.Name, Is.EqualTo(entity.Name));
            Assert.That(game.MinimumPlayers, Is.EqualTo(entity.MinimumPlayers));
            Assert.That(game.MaximumPlayers, Is.EqualTo(entity.MaximumPlayers));
            Assert.That(game.Id, Is.EqualTo(entity.Id));
        }

        [Test]
        public async Task Edit_IdInDb_ReturnsTrue()
        {
            Game entity = CreateEntity(VALID_STRING_OBJECT_ID);
            GameService service = GetService();
            await AddEntityToCollection(entity, service);

            entity.Name = "Game Edited";

            // Act
            bool success = await service.EditAsync(entity);

            Assert.IsTrue(success);
        }

        [Test]
        public async Task Edit_IdNotInDb_ReturnsFalse()
        {
            Game updatedGame = CreateEntity(VALID_STRING_OBJECT_ID);
            GameService service = GetService();

            // Act
            bool success = await service.EditAsync(updatedGame);

            Assert.IsFalse(success);
        }

        [Test]
        public async Task FindByUrl_ValidUrl_ReturnsMatchingGame()
        {
            Game entity = CreateEntity(VALID_STRING_OBJECT_ID);

            GameService service = GetService();

            await AddEntityToCollection(entity, service);

            // Act
            Game retrieved = await service.FindByUrlAsync(entity.Url);

            Assert.IsNotNull(retrieved);
            Assert.That(retrieved.Id, Is.EqualTo(entity.Id));
            Assert.That(retrieved.Name, Is.EqualTo(entity.Name));
            Assert.That(retrieved.MinimumPlayers, Is.EqualTo(entity.MinimumPlayers));
            Assert.That(retrieved.MaximumPlayers, Is.EqualTo(entity.MaximumPlayers));
            Assert.That(retrieved.Url, Is.EqualTo(entity.Url));
        }

        [Test]
        public async Task FindByUrl_NonExistantUrl_ReturnsNull()
        {
            GameService service = GetService();

            // Act
            Game retrieved = await service.FindByUrlAsync("a-non-existant-url");

            Assert.IsNull(retrieved);
        }
    }
}
