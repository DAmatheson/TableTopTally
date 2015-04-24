/* MongoHelper.cs
* 
* Purpose: A helper class to simplify getting mongoDB collections
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/

using System.Configuration;
using MongoDB.Driver;
using TableTopTally.DataModels.MongoDB.Entities;

namespace TableTopTally.MongoDataAccess
{
    /// <summary>
    /// A helper class for connecting to MongoDB and getting MongoDB collections
    /// </summary>
    public static class MongoHelper
    {
        private static readonly MongoDatabase dbTableTopTally;

        /// <summary>
        /// Initializes the static properties of the class
        /// </summary>
        static MongoHelper()
        {
            var connString = ConfigurationManager.ConnectionStrings["MongoTableTopTally"].ConnectionString;
            var url = new MongoUrl(connString);

            var client = new MongoClient(url);
            var server = client.GetServer();

            dbTableTopTally = server.GetDatabase(url.DatabaseName);
        }

        /// <summary>
        /// Gets the collection for type T from the tableTopTally MongoDB database with a default
        /// document type of T
        /// </summary>
        /// <typeparam name="T">The default document type and the collection to get</typeparam>
        /// <returns>The collection for type T</returns>
        public static MongoCollection<T> GetTableTopCollection<T>() where T : IMongoEntity
        {
            return dbTableTopTally.GetCollection<T>(typeof (T).Name.ToLower() + 's');
        }
    }
}