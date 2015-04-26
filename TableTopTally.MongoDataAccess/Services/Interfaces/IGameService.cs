/* IGameService.cs
 * 
 * Purpose: Interface for database services for the Game model
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using TableTopTally.DataModels.Models;

namespace TableTopTally.MongoDataAccess.Services
{
    public interface IGameService : IMongoService<Game>
    {
        Task<bool> EditAsync(Game game);

        Task<IEnumerable<Game>> GetGamesAsync();

        Task<Game> FindByUrlAsync(string gameUrl);
    }
}