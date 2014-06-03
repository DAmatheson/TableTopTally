/* IVariantService.cs
 * 
 * Purpose: Interface for database services for the Variant model
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */ 

using System.Collections.Generic;
using MongoDB.Bson;
using TableTopTally.Models;

namespace TableTopTally.MongoDB.Services
{
    interface IVariantService
    {
        bool Create(ObjectId gameId, Variant variant);

        bool Edit(ObjectId gameId, Variant variant);

        bool Delete(ObjectId gameId, ObjectId variantId);

        Game GetVariantByUrl(string gameUrl, string variantUrl);

        Game GetVariantById(ObjectId gameId, ObjectId variantId);

        IEnumerable<Variant> GetVariants(string gameUrl);
    }
}
