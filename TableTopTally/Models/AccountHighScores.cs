using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TableTopTally.MongoDB.Entities;

namespace TableTopTally.Models
{
    public class AccountHighScores : IMongoEntity
    {
        // Account Id
        [BsonId]
        public ObjectId Id { get; set; }

        public List<GameHighScore> GameHighScores { get; set; }
    }

    public class GameHighScore 
    {
        public ObjectId GameId { get; set; }

        public List<VariantHighScore> VariantHighScores { get; set; }
    }

    public class VariantHighScore
    {
        public ObjectId VariantId { get; set; }

        public int NumberOfPlayers { get; set; }

        public ObjectId ScorerId { get; set; }

        public double SessionScore { get; set; }

        public List<RoundHighScore> RoundHighScores { get; set; }
    }

    public class RoundHighScore
    {
        public ObjectId ScorerId { get; set; }

        public double RoundScore { get; set; }

        public int RoundNumber { get; set; }

        public List<ScoreItemHighScore> ScoreItemsHighScores { get; set; } 
    }

    public class ScoreItemHighScore
    {
        public ObjectId ScoreItemId { get; set; }

        public ObjectId ScorerId { get; set; }

        public double ItemScore { get; set; }
    }
}