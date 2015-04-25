/* IGameVariantService.cs
 * 
 * Purpose: Interface for database services for the GameVariant model
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */

using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.DataModels.Models;

namespace TableTopTally.MongoDataAccess.Services
{
    interface IGameVariantService : IMongoService<GameVariant>
    {
        bool Edit(GameVariant variant);

        IEnumerable<GameVariant> FindGameVariants(ObjectId gameId);
    }
}
