using Azure.Data.Tables;
using Azure;
using System.Linq.Expressions;
using ServiceQuery;

namespace ServiceQuery.Xunit.Integration
{
    public class AggregateAverageTestsAsync : BaseTest<AzureDataTablesTestClass>
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
        public async Task AverageStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();

            // Int
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.IntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.IntVal)).Build();

            // Long
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.LongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.LongVal)).Build();
        }

        [Fact]
        public async Task AverageRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.IntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.LongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
        }

        [Fact]
        public async Task NullAverageStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();

            // Int
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();

            // Long
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
        }

        [Fact]
        public async Task NullAverageRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
        }
    }
}