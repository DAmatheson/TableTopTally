using System.Configuration;
using MongoDB.Driver;

namespace TableTopTally.Helpers
{
    public class MongoHelper<T> where T : class
    {
        /// <summary>
        /// The mongoDB collection for type T
        /// </summary>
        public MongoCollection<T> Collection { get; private set; }

        /// <summary>
        /// Initializes a instance of the MongoHelper class
        /// </summary>
        public MongoHelper()
        {
            var con = new MongoConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["MongoTableTopTally"].ConnectionString
            );

            var client = new MongoClient(con.ConnectionString);
            var server = client.GetServer();
            var db = server.GetDatabase(con.DatabaseName);

            Collection = db.GetCollection<T>(typeof(T).Name.ToLower());
        }
    }
}