using Azure.Data.Tables;
using Azure;
using System.Linq.Expressions;
using ServiceQuery;

namespace ServiceQuery.Xunit
{
    public class AggregateAverageTests : BaseTest<AzureDataTablesTestClass>
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

        public override IQueryable<AzureDataTablesTestClass> GetTestList()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task AverageStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.IntVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.IntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.LongVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.LongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
        }

        [Fact]
        public async Task AverageRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.IntVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.LongVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == (double)6 / 4);
        }

        [Fact]
        public async Task NullAverageStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
        }

        [Fact]
        public async Task NullAverageRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
            response = await serviceQuery.ExecuteAsync<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == null);
        }
    }
}