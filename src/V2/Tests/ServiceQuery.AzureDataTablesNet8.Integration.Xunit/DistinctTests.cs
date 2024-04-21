using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit
{
    public class DistinctTests : BaseTest<AzureDataTablesTestClass>
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
        public void DistinctStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForDistinct = true };

            // NOT SUPPORTED

            serviceQuery = ServiceQueryBuilder.New().Select(nameof(TestClass.SharedParentKey)).Distinct().Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.List.Count == 4);
        }

        [Fact]
        public void DistinctRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForDistinct = true };

            // NOT SUPPORTED

            serviceQuery = ServiceQueryRequestBuilder.New().Select(nameof(TestClass.SharedParentKey)).Distinct().Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.List.Count == 4);
        }
    }
}