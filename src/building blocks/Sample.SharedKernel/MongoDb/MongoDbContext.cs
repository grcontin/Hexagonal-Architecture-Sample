using MongoDB.Driver;

namespace Sample.SharedKernel.MongoDb
{
    public sealed class MongoDbContext
    {
        public IMongoClient MongoClient { get; }
        public IMongoDatabase Database { get; }

        public MongoDbContext(string databaseName, MongoClient mongoClient)
        {
            MongoClient = mongoClient;
            Database = GetMongoDatabase(databaseName);
        }


        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentException("The collection name must have a value");

            return Database.GetCollection<T>(collectionName);
        }
        private IMongoDatabase GetMongoDatabase(string databaseName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(databaseName))
                    throw new ArgumentException($"{nameof(databaseName)} must have a value");

                return MongoClient.GetDatabase(databaseName);
            }
            catch (Exception ex)
            {
                throw new MongoException("Unable to connect to MongoDB", ex);
            }
        }
    }
}
