using Azure;
using Azure.Data.Tables;

namespace ServiceQuery.Xunit.Integration
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesSortTestsAsync : BaseTest
    {
        private TableClient _tableClient = null;

        protected async Task Startup()
        {
            _tableClient = new TableClient(AzureDataTablesHelper.GetConnectionString(), "ServiceQueryTestClasses");
            _tableClient.CreateIfNotExists();
            List<AzureDataTablesTestClass> testClasses = new List<AzureDataTablesTestClass>();
            var asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(maxPerPage: 1000);
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testClasses.AddRange(page.Values);
            if (testClasses.Count != 4)
            {
                var testlist = AzureDataTablesTestClass.GetDefaultList();
                foreach (var item in testlist)
                    _tableClient.AddEntity(item);
            }
        }

        //[Fact]
        //public async Task SortAscTest()
        //{
        //    // NOT SUPPORTED
        //}

        //[Fact]
        //public async Task SortDescTest()
        //{
        // NOT SUPPORTED
        //}
    }
}