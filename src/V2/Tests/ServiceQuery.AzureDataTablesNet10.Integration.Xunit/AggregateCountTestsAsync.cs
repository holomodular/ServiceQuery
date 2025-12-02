using Azure.Data.Tables;
using Azure;

namespace ServiceQuery.Xunit.Integration
{
    public class AggregateCountTestsAsync : BaseTest<AzureDataTablesTestClass>
    {
        public override IQueryable<AzureDataTablesTestClass> GetTestList()
        {
            throw new NotImplementedException();
        }

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

        [Fact]
        public async Task CountStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            serviceQuery = ServiceQueryBuilder.New().Count().Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 4);
            serviceQuery = ServiceQueryBuilder.New().Count().Build();

            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Count().Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 1);
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Count().Build();

            serviceQuery = ServiceQueryBuilder.New().Count().Paging(1, 1000, true).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 4);
            Assert.True(response.Count == 4);
            serviceQuery = ServiceQueryBuilder.New().Count().Paging(1, 1000, true).Build();

            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Count().Paging(1, 1000, true).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 1);
            Assert.True(response.Count == 1);
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Count().Paging(1, 1000, true).Build();
        }

        [Fact]
        public async Task CountRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            serviceQuery = ServiceQueryRequestBuilder.New().Count().Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 4);
            Assert.True(response.Count == null);
            serviceQuery = ServiceQueryRequestBuilder.New().Count().Build();

            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Count().Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 1);
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Count().Build();

            serviceQuery = ServiceQueryRequestBuilder.New().Count().Paging(1, 1000, true).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 4);
            Assert.True(response.Count == 4);
            serviceQuery = ServiceQueryRequestBuilder.New().Count().Paging(1, 1000, true).Build();

            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Count().Paging(1, 1000, true).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 1);
            Assert.True(response.Count == 1);
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Count().Paging(1, 1000, true).Build();
        }
    }
}