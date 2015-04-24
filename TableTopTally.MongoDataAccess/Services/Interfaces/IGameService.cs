/* IGameService.cs
 * 
 * Purpose: Interface for database services for the Game model
 * 
 * Revision History:
 *      Drew Matheson, 2014.06.03: Created
 */

using System.Collections.Generic;
using TableTopTally.DataModels.Models;

namespace TableTopTally.MongoDataAccess.Services
{
    public interface IGameService : IMongoService<Game>
    {
        bool Edit(Game game);

        IEnumerable<Game> GetGames();

        Game FindByUrl(string gameUrl);
    }
}