using MongoDB.Driver;

namespace WebApp.Database
{
    public class MongoDbDatabaseContext
    {
        private MongoClient _client;
        private string _connectionString;
        public IMongoCollection<ExampleClass> ExampleClasses;

        public MongoDbDatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MongoDb");
        }

        public void Load()
        {
            if (ExampleClasses == null)
            {
                _client = new MongoClient(_connectionString);
                var mongoDatabase = _client.GetDatabase("ServiceQuery");
                ExampleClasses = mongoDatabase.GetCollection<ExampleClass>("ExampleClasses");
            }
        }

        public IQueryable<ExampleClass> ExampleClassesAsQueryable()
        {
            if (ExampleClasses == null)
                Load();
            return ExampleClasses.AsQueryable();
        }
    }
}