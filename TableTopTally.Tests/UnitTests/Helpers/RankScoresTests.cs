using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.Helpers;
using TableTopTally.Models;

namespace TableTopTally.Tests.UnitTests.Helpers
{
    [TestFixture]
    class RankScoresTests
    {
        private const string FIRST_PLAYER_ID = "53e3a8ad6c46bc0c80ea13b2";
        private const string SECOND_PLAYER_ID = "54a07c8a4a91a323e83d78d2";
        private const string THIRD_PLAYER_ID = "54a4290ed968bc127cdeaf2e";
        private const string SCORING_ITEM_ID = "52e3a8ad6c46bc0c80ea13b2";

        private Ranking CreateRanking(string objectId, int score)
        {
            return new Ranking
            {
                PlayerId = new ObjectId(objectId),
                Score = score
            };
        }

        private Player CreatePlayer(string objectId, string name)
        {
            return new Player
            {
                Id = new ObjectId(objectId),
                Name = name
            };
        }

        private PlayerScore CreatePlayerScore(string playerId, int score)
        {
            return new PlayerScore
            {
                PlayerId = new ObjectId(playerId),
                ItemScores = new Dictionary<ObjectId, double>
                {
                    { new ObjectId(SCORING_ITEM_ID), score }
                }
            };
        }

        [Test]
        public void Descending_ThreeRankings_ReturnsOrderedRankings()
        {
            List<Ranking> rankings = new List<Ranking>
            {
                CreateRanking(FIRST_PLAYER_ID, 1),
                CreateRanking(SECOND_PLAYER_ID, 2),
                CreateRanking(THIRD_PLAYER_ID, 3)
            };

            // Act
            List<Ranking> result = RankScores.Descending(rankings).ToList();

            Assert.That(result[2].PlayerId, Is.EqualTo(new ObjectId(FIRST_PLAYER_ID)));
            Assert.That(result[1].PlayerId, Is.EqualTo(new ObjectId(SECOND_PLAYER_ID)));
            Assert.That(result[0].PlayerId, Is.EqualTo(new ObjectId(THIRD_PLAYER_ID)));
        }

        [Test]
        public void Descending_FourRankings_ReturnsTopThreeOrderedRankings()
        {
            List<Ranking> rankings = new List<Ranking>
            {
                CreateRanking(FIRST_PLAYER_ID, 1),
                CreateRanking(SECOND_PLAYER_ID, 2),
                CreateRanking(THIRD_PLAYER_ID, 3),
                CreateRanking("000000000000000000000000", 0)
            };

            // Act
            List<Ranking> result = RankScores.Descending(rankings).ToList();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[2].PlayerId, Is.EqualTo(new ObjectId(FIRST_PLAYER_ID)));
            Assert.That(result[1].PlayerId, Is.EqualTo(new ObjectId(SECOND_PLAYER_ID)));
            Assert.That(result[0].PlayerId, Is.EqualTo(new ObjectId(THIRD_PLAYER_ID)));
        }

        [Test]
        public void Descending_EmptyInput_ReturnsEmptyEnumerable()
        {
            List<Ranking> rankings = new List<Ranking>();

            // Act
            IEnumerable<Ranking> result = RankScores.Descending(rankings);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Descending_NullInput_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => RankScores.Descending(null).GetEnumerator().MoveNext());
        }

        [Test]
        public void RankSession_ThreePlayers_ReturnsOrderedRankings()
        {
            PlaySession session = new PlaySession
            {
                Players = new List<Player>
                {
                    CreatePlayer(FIRST_PLAYER_ID, "a"),
                    CreatePlayer(SECOND_PLAYER_ID, "b"),
                    CreatePlayer(THIRD_PLAYER_ID, "c")
                },
                Rounds = new List<Round>
                {
                    new Round
                    {
                        Number = 1,
                        Scores = new List<PlayerScore>
                        {
                            CreatePlayerScore(FIRST_PLAYER_ID, 1),
                            CreatePlayerScore(SECOND_PLAYER_ID, 2),
                            CreatePlayerScore(THIRD_PLAYER_ID, 3),
                        }
                    }
                }
            };

            IEnumerable<Ranking> rankings = RankScores.RankSession(session);

            Assert.IsNotNull(rankings);

            List<Ranking> rankingsList = rankings.ToList();

            Assert.That(rankingsList.Count, Is.EqualTo(3));
            Assert.That(rankingsList[0].PlayerId, Is.EqualTo(new ObjectId(THIRD_PLAYER_ID)));
            Assert.That(rankingsList[1].PlayerId, Is.EqualTo(new ObjectId(SECOND_PLAYER_ID)));
            Assert.That(rankingsList[2].PlayerId, Is.EqualTo(new ObjectId(FIRST_PLAYER_ID)));
        }

        [Test]
        public void RankSession_EmptySession_ReturnsEmptyEnumerable()
        {
            PlaySession session = new PlaySession
            {
                Players = new List<Player>(),
                Rounds = new List<Round>()
            };

            IEnumerable<Ranking> rankings = RankScores.RankSession(session);

            Assert.IsNotNull(rankings);
            Assert.That(rankings, Is.Empty);
        }

        [Test]
        public void RankSession_NullSession_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => RankScores.RankSession(null));
        }
    }
}
