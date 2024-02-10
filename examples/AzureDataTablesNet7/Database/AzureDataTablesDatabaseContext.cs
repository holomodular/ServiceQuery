using Azure.Data.Tables;

namespace WebApp.Database
{
    public class AzureDataTablesDatabaseContext
    {
        private TableClient _tableClient = null;
        private string _connectionString = string.Empty;

        public AzureDataTablesDatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureDataTables");
        }

        public void Load()
        {
            _tableClient = new TableClient(_connectionString, "ServiceQueryExamples");
            _tableClient.CreateIfNotExists();
        }

        public TableClient ExampleClassesTableClient()
        {
            if (_tableClient == null)
                Load();
            return _tableClient;
        }
    }
}