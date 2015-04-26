/* GameVariantService.cs
* 
* Purpose: A class with methods for CRUDing a Game's variant documents in MongoDB
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TableTopTally.DataModels.Models;

namespace TableTopTally.MongoDataAccess.Services
{
    /// <summary>
    /// Provides methods for CRUDing game variants in a mongoDB database
    /// </summary>
    public class GameVariantService : MongoService<GameVariant>, IGameVariantService
    {
        /// <summary>
        /// Updates the variant that belongs to the specified game
        /// </summary>
        /// <param name="variant">GameVariant representing the variant to update</param>
        /// <returns>Returns a bool representing if the edit completed successfully</returns>
        public async Task<bool> EditAsync(GameVariant variant)
        {
            UpdateResult result = await collection.UpdateOneAsync(
                Builders<GameVariant>.Filter.
                    Eq(gv => gv.Id, variant.Id),
                Builders<GameVariant>.Update.
                    Set(gv => gv.Name, variant.Name).
                    Set(gv => gv.Url, variant.Url).
                    Set(gv => gv.TrackScores, variant.TrackScores).
                    Set(gv => gv.ScoreItems, variant.ScoreItems));

            return result.ModifiedCount >= 1;
        }

        public async Task<IEnumerable<GameVariant>> FindGameVariantsAsync(ObjectId gameId)
        {
            // Unsure: Throw argument exception or just return empty enumerable
            if (gameId == ObjectId.Empty)
                return Enumerable.Empty<GameVariant>();

            return await collection.
                Find(Builders<GameVariant>.Filter.Eq(gv => gv.GameId, gameId)).ToListAsync();
        }
    }
}