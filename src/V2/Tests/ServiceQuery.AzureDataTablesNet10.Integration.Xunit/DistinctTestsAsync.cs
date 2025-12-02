using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit.Integration
{
    public class DistinctTestsAsync : BaseTest<AzureDataTablesTestClass>
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

        public override IQueryable<AzureDataTablesTestClass> GetTestList()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task DistinctStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForDistinct = true };

            // NOT SUPPORTED

            serviceQuery = ServiceQueryBuilder.New().Select(nameof(TestClass.SharedParentKey)).Distinct().Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.List.Count == 4);
        }

        [Fact]
        public async Task DistinctRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForDistinct = true };

            // NOT SUPPORTED

            serviceQuery = ServiceQueryRequestBuilder.New().Select(nameof(TestClass.SharedParentKey)).Distinct().Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.List.Count == 4);
        }
    }
}