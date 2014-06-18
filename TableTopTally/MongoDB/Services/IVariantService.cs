/* IVariantService.cs
 * 
 * Purpose: Interface for database services for the Variant model
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */ 

using TableTopTally.Models;

namespace TableTopTally.MongoDB.Services
{
    interface IVariantService : IMongoService<Variant>
    {
        bool Edit(Variant variant);
    }
}
