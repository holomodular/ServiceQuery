using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit.Integration
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesComparisonIsInSetTestsAsync : BaseTest
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

        [Fact]
        public async Task IsInSetStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.BoolVal), "true").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            //// Decimal
            //serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DecimalVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 1);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DoubleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.FloatVal), "1").Build();
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.IntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.LongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.SingleVal), "1").Build();
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.StringVal), "a").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
        }

        [Fact]
        public async Task IsInSetTwo()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.BoolVal), "true", "false").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 4);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                var tempDateTimeOffset2 = new TestClass().GetDefault2Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            }

            // DateTime
            var tempDateTime1 = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            var tempDateTime2 = new TestClass().GetDefault2Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            //// Decimal
            //serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DecimalVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DoubleVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Guid
            var tempGuid = new TestClass().GetDefault1Record(new TestClass()).GuidVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempGuid.ToString()).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.IntVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.LongVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.StringVal), "a", "b").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);
        }

        [Fact]
        public async Task IsInSetRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.BoolVal), "true").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            //// Decimal
            //serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DecimalVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 1);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DoubleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.IntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.LongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.StringVal), "a").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
        }

        [Fact]
        public async Task IsInSetTwoRequest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.BoolVal), "true", "false").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 4);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                var tempDateTimeOffset2 = new TestClass().GetDefault2Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            }

            // DateTime
            var tempDateTime1 = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            var tempDateTime2 = new TestClass().GetDefault2Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            //// Decimal
            //serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DecimalVal), "1", "2").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DoubleVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Guid
            var tempGuid = new TestClass().GetDefault1Record(new TestClass()).GuidVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempGuid.ToString()).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.IntVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.LongVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.StringVal), "a", "b").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);
        }

        [Fact]
        public async Task NullIsInSetStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullBoolVal), "true").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// Decimal
            //serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDoubleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullStringVal), "a").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);
        }

        [Fact]
        public async Task NullIsInSetRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullBoolVal), "true").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// Decimal
            //serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDoubleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullStringVal), "a").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);
        }

        [Fact]
        public async Task NullIsInSetRequestAllTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullBoolVal), null).Build();
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateTimeOffsetVal), null).Build();
                await Assert.ThrowsAsync<RequestFailedException>(async () =>
                {
                    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                    testList = new List<AzureDataTablesTestClass>();
                    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                        testList.AddRange(page.Values);
                    Assert.NotNull(testList);
                    Assert.True(testList.Count == 4);
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateTimeVal), null).Build();
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            //// Decimal
            //serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDecimalVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDoubleVal), null).Build();
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullGuidVal), null).Build();
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullIntVal), null).Build();
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullLongVal), null).Build();
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullStringVal), null).Build();
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });
        }
    }
}