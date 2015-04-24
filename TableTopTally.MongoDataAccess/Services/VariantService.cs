/* VariantService.cs
* 
* Purpose: A class with methods for CRUDing a Game's variant documents in MongoDB
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/

using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TableTopTally.DataModels.Models;

namespace TableTopTally.MongoDataAccess.Services
{
    /// <summary>
    /// Provides methods for CRUDing game variants in a mongoDB database
    /// </summary>
    public class VariantService : MongoService<Variant>, IVariantService
    {
        private readonly MongoCollection<Variant> variantCollection;

        /// <summary>
        ///     Initializes a new instance of the VariantService class
        /// </summary>
        public VariantService()
        {
            variantCollection = MongoHelper.GetTableTopCollection<Variant>();
        }

        /// <summary>
        /// Updates the variant that belongs to the specified game
        /// </summary>
        /// <param name="variant">Variant representing the variant to update</param>
        /// <returns>Returns a bool representing if the edit completed successfully</returns>
        public bool Edit(Variant variant)
        {
            WriteConcernResult result = variantCollection.Update(
                Query.EQ("_id", variant.Id),
                Update.
                    Set("Name", variant.Name).
                    Set("Url", variant.Url).
                    Set("TrackScores", variant.TrackScores).
                    SetWrapped("ScoreItems", variant.ScoreItems));

            return !result.HasLastErrorMessage && result.DocumentsAffected >= 1;
        }

        public IEnumerable<Variant> FindGameVariants(ObjectId gameId)
        {
            // Unsure: Throw argument exception or just return empty enumerable
            if (gameId == ObjectId.Empty)
                return Enumerable.Empty<Variant>();

            return collection.Find(Query.EQ("GameId", gameId));
        }
    }
}