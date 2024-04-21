using Azure;
using Azure.Data.Tables;

namespace ServiceQuery.Xunit
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesSortTests : BaseTest
    {
        private TableClient _tableClient = null;

        protected void Startup()
        {
            _tableClient = new TableClient(AzureDataTablesHelper.GetConnectionString(), "ServiceQueryTestClasses");
            _tableClient.CreateIfNotExists();
            List<AzureDataTablesTestClass> testClasses = new List<AzureDataTablesTestClass>();
            var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(maxPerPage: 1000);
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
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