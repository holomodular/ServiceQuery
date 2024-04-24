using Azure;
using Azure.Data.Tables;

namespace ServiceQuery.Xunit.Integration
{
    public class SortTests : BaseTest<AzureDataTablesTestClass>
    {
        private TableClient _tableClient = null;

        public override IQueryable<AzureDataTablesTestClass> GetTestList()
        {
            throw new NotImplementedException();
        }

        protected void Startup()
        {
            _tableClient = new TableClient(AzureDataTablesHelper.GetConnectionString(), "ServiceQueryTestClasses");
            _tableClient.CreateIfNotExists();
            List<AzureDataTablesTestClass> testClasses = new List<AzureDataTablesTestClass>();
            var pagableresponse = _tableClient.Query<AzureDataTablesTestClass>(maxPerPage: 1000);
            foreach (Page<AzureDataTablesTestClass> page in pagableresponse.AsPages())
                testClasses.AddRange(page.Values);
            if (testClasses.Count != 4)
            {
                var testlist = AzureDataTablesTestClass.GetDefaultList();
                foreach (var item in testlist)
                    _tableClient.AddEntity(item);
            }
        }

        protected void ValidateSort(ServiceQueryResponse<AzureDataTablesTestClass> response, bool asc)
        {
            Assert.NotNull(response.List);
            Assert.True(response.List.Count == 4);
            if (asc)
            {
                Assert.True(response.List[0].IntVal == 0);
                Assert.True(response.List[1].IntVal == 1);
                Assert.True(response.List[2].IntVal == 2);
                Assert.True(response.List[3].IntVal == 3);
            }
            else
            {
                Assert.True(response.List[0].IntVal == 3);
                Assert.True(response.List[1].IntVal == 2);
                Assert.True(response.List[2].IntVal == 1);
                Assert.True(response.List[3].IntVal == 0);
            }
        }

        [Fact]
        public void SortAscTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForSort = true };

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.BoolVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.List[0].IntVal == 0 || response.List[0].IntVal == 2);
            Assert.True(response.List[1].IntVal == 0 || response.List[1].IntVal == 2);
            Assert.True(response.List[2].IntVal == 1 || response.List[2].IntVal == 3);
            Assert.True(response.List[3].IntVal == 1 || response.List[3].IntVal == 3);

            serviceQuery = ServiceQueryBuilder.New().SortAsc(nameof(AzureDataTablesTestClass.BoolVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.List[0].IntVal == 0 || response.List[0].IntVal == 2);
            Assert.True(response.List[1].IntVal == 0 || response.List[1].IntVal == 2);
            Assert.True(response.List[2].IntVal == 1 || response.List[2].IntVal == 3);
            Assert.True(response.List[3].IntVal == 1 || response.List[3].IntVal == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), true).Build();
                response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

                ValidateSort(response, true);
            }

            // DateTime
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.DateTimeVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.DoubleVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);

            ValidateSort(response, true);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.GuidVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.IntVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.LongVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.StringVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);
        }

        [Fact]
        public void SortDescTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForSort = true };

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.BoolVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);
            Assert.True(response.List[0].IntVal == 1 || response.List[0].IntVal == 3);
            Assert.True(response.List[1].IntVal == 1 || response.List[1].IntVal == 3);
            Assert.True(response.List[2].IntVal == 0 || response.List[2].IntVal == 2);
            Assert.True(response.List[3].IntVal == 0 || response.List[3].IntVal == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);
            //response = await testQueryable.ToListAsync();
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), false).Build();
                response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

                ValidateSort(response, false);
            }

            // DateTime
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.DateTimeVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.DoubleVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.GuidVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.IntVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.LongVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.StringVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);
        }

        [Fact]
        public void SortAscRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForSort = true };

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.BoolVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.List[0].IntVal == 0 || response.List[0].IntVal == 2);
            Assert.True(response.List[1].IntVal == 0 || response.List[1].IntVal == 2);
            Assert.True(response.List[2].IntVal == 1 || response.List[2].IntVal == 3);
            Assert.True(response.List[3].IntVal == 1 || response.List[3].IntVal == 3);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);
            //response = await testQueryable.ToListAsync();
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), true).Build();
                response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

                ValidateSort(response, true);
            }

            // DateTime
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.DateTimeVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.DoubleVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.GuidVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.IntVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.LongVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.StringVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, true);
        }

        [Fact]
        public void SortDescRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForSort = true };

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.BoolVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.List[0].IntVal == 1 || response.List[0].IntVal == 3);
            Assert.True(response.List[1].IntVal == 1 || response.List[1].IntVal == 3);
            Assert.True(response.List[2].IntVal == 0 || response.List[2].IntVal == 2);
            Assert.True(response.List[3].IntVal == 0 || response.List[3].IntVal == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);
            //response = await testQueryable.ToListAsync();
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), false).Build();
                response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

                ValidateSort(response, false);
            }

            // DateTime
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.DateTimeVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.DoubleVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.GuidVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.IntVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.LongVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.StringVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            ValidateSort(response, false);
        }

        [Fact]
        public void NullSortAscTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForSort = true };

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullBoolVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            //Assert.True(response.List[0].IntVal == 0 || response.List[0].IntVal == 2);
            //Assert.True(response.List[1].IntVal == 0 || response.List[1].IntVal == 2);
            //Assert.True(response.List[2].IntVal == 1 || response.List[2].IntVal == 3);
            //Assert.True(response.List[3].IntVal == 1 || response.List[3].IntVal == 3);

            serviceQuery = ServiceQueryBuilder.New().SortAsc(nameof(AzureDataTablesTestClass.NullBoolVal)).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);
            //Assert.True(response.List[0].IntVal == 0 || response.List[0].IntVal == 2);
            //Assert.True(response.List[1].IntVal == 0 || response.List[1].IntVal == 2);
            //Assert.True(response.List[2].IntVal == 1 || response.List[2].IntVal == 3);
            //Assert.True(response.List[3].IntVal == 1 || response.List[3].IntVal == 3);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);
            //response = await testQueryable.ToListAsync();
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), true).Build();
                response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

                Assert.True(response.Count == null);
            }

            // DateTime
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDateTimeVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDoubleVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullGuidVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullIntVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullLongVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullStringVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);
        }

        [Fact]
        public void NullSortDescTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForSort = true };

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullBoolVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);
            //response = await testQueryable.ToListAsync();
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), false).Build();
                response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

                Assert.True(response.Count == null);
            }

            // DateTime
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDateTimeVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDoubleVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullGuidVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullIntVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullLongVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullStringVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);
        }

        [Fact]
        public void NullSortAscRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForSort = true };

            // NOT SUPPORTED FOR NULL

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullBoolVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);
            //response = await testQueryable.ToListAsync();
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), true).Build();
                response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

                Assert.True(response.Count == null);
            }

            // DateTime
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDateTimeVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDoubleVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullGuidVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullIntVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullLongVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullStringVal), true).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);
        }

        [Fact]
        public void NullSortDescRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<AzureDataTablesTestClass> response;
            var serviceQueryOptions = new ServiceQueryOptions();
            var azureOptions = new AzureDataTablesOptions() { DownloadAllRecordsForSort = true };

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullBoolVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);
            //response = await testQueryable.ToListAsync();
            //Assert.NotNull(response);
            //Assert.True(response.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), false).Build();
                response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

                Assert.True(response.Count == null);
            }

            // DateTime
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDateTimeVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullDoubleVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullGuidVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullIntVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullLongVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(AzureDataTablesTestClass.NullStringVal), false).Build();
            response = serviceQuery.Execute<AzureDataTablesTestClass>(_tableClient, serviceQueryOptions, azureOptions);

            Assert.True(response.Count == null);
        }
    }
}