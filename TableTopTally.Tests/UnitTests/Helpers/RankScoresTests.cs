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

        [Test]
        public void Descending_ThreeRankings_ReturnsOrderedRankings()
        {
            List<Ranking> rankings = new List<Ranking>
            {
                new Ranking
                {
                    PlayerId = new ObjectId(FIRST_PLAYER_ID),
                    Score = 1
                },
                new Ranking
                {
                    PlayerId = new ObjectId(SECOND_PLAYER_ID),
                    Score = 2
                },
                new Ranking
                {
                    PlayerId = new ObjectId(THIRD_PLAYER_ID),
                    Score = 3
                }
            };

            // Act
            List<Ranking> result = RankScores.Descending(rankings).ToList();

            Assert.That(new ObjectId(FIRST_PLAYER_ID), Is.EqualTo(result[2].PlayerId));
            Assert.That(new ObjectId(SECOND_PLAYER_ID), Is.EqualTo(result[1].PlayerId));
            Assert.That(new ObjectId(THIRD_PLAYER_ID), Is.EqualTo(result[0].PlayerId));
        }

        [Test]
        public void Descending_FourRankings_ReturnsTopThreeOrderedRankings()
        {
            List<Ranking> rankings = new List<Ranking>
            {
                new Ranking
                {
                    PlayerId = new ObjectId(FIRST_PLAYER_ID),
                    Score = 1
                },
                new Ranking
                {
                    PlayerId = new ObjectId(SECOND_PLAYER_ID),
                    Score = 2
                },
                new Ranking
                {
                    PlayerId = new ObjectId(THIRD_PLAYER_ID),
                    Score = 3
                },
                new Ranking
                {
                    Score = 0
                }
            };

            // Act
            List<Ranking> result = RankScores.Descending(rankings).ToList();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(new ObjectId(FIRST_PLAYER_ID), Is.EqualTo(result[2].PlayerId));
            Assert.That(new ObjectId(SECOND_PLAYER_ID), Is.EqualTo(result[1].PlayerId));
            Assert.That(new ObjectId(THIRD_PLAYER_ID), Is.EqualTo(result[0].PlayerId));
        }

        [Test]
        public void Descending_EmptyInput_ReturnsEmptyEnumerable()
        {
            List<Ranking> rankings = new List<Ranking>();

            // Act
            IEnumerable<Ranking> result = RankScores.Descending(rankings);

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Descending_NullInput_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => RankScores.Descending(null).GetEnumerator().MoveNext());
        }
    }
}
