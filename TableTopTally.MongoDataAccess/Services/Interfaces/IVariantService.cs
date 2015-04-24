/* IVariantService.cs
 * 
 * Purpose: Interface for database services for the Variant model
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */

using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.DataModels.Models;

namespace TableTopTally.MongoDataAccess.Services
{
    interface IVariantService : IMongoService<Variant>
    {
        bool Edit(Variant variant);

        IEnumerable<Variant> FindGameVariants(ObjectId gameId);
    }
}
