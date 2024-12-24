using Azure.Data.Tables;
using Azure;

namespace ServiceQuery.Xunit.Integration
{
    public class AggregateSumTests : BaseTest<AzureDataTablesTestClass>
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
        public void SumStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DoubleVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 6);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.IntVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 6);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.LongVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 6);
        }

        [Fact]
        public void SumRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DoubleVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 6);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.IntVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 6);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.LongVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 6);
        }

        [Fact]
        public void NullSumStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDoubleVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullIntVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullLongVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);
        }

        [Fact]
        public void NullSumRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForAggregate = true };

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDoubleVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullIntVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullLongVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.Aggregate == 0);
        }
    }
}