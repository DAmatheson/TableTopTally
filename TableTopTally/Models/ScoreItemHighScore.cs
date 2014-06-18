using MongoDB.Bson;

namespace TableTopTally.Models
{
    public class ScoreItemHighScore
    {
        /// <summary>
        /// The ScoreItem's id
        /// </summary>
        public ObjectId ScoreItemId { get; set; }

        /// <summary>
        /// Player id of highest scorer for the ScoreItem
        /// </summary>
        public ObjectId ScorerId { get; set; }

        /// <summary>
        /// Highest score for the ScoreItem
        /// </summary>
        public double ItemScore { get; set; }
    }
}