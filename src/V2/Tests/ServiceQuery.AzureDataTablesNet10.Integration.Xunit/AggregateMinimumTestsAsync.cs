using Azure.Data.Tables;
using Azure;

namespace ServiceQuery.Xunit.Integration
{
    public class AggregateMinimumTestsAsync : BaseTest<AzureDataTablesTestClass>
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
        public async Task MinimumStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.DoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.IntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.LongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);
        }

        [Fact]
        public async Task MinimumRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.DoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.IntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.LongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);
        }

        [Fact]
        public async Task NullMinimumStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullIntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullLongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
        }

        [Fact]
        public async Task NullMinimumRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullIntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullLongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
        }
    }
}