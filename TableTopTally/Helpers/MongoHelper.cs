/* MongoHelper.cs
* 
* Purpose: A helper class to simplify getting mongoDB collections
* 
* Revision History:
*      Drew Matheson, 2014.05.29: Created
*/ 

using System.Configuration;
using MongoDB.Driver;

namespace TableTopTally.Helpers
{
    public static class MongoHelper
    {
        private static readonly MongoDatabase dbTableTopTally;

        /// <summary>
        /// Initializes the static properties of the class
        /// </summary>
        static MongoHelper()
        {
            var conn = new MongoConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["MongoTableTopTally"].ConnectionString
                );

            var client = new MongoClient(conn.ConnectionString);
            var server = client.GetServer();
            dbTableTopTally = server.GetDatabase(conn.DatabaseName);
        }

        /// <summary>
        /// Gets and returns the collection for type T from mongoDB
        /// </summary>
        /// <typeparam name="T">The type of the collection you wish to get</typeparam>
        /// <returns>The collection in Mongo's TableTopTally db for type T</returns>
        public static MongoCollection<T> GetTableTopCollection<T>() where T : class
        {
            return dbTableTopTally.GetCollection<T>(typeof (T).Name.ToLower() + 's');
        }
    }
}