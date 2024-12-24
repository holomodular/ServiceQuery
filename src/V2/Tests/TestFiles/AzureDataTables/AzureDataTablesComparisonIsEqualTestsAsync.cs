using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit.Integration
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesComparisonIsEqualTestsAsync : BaseTest
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
        public async Task IsEqualStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
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
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 1);
            //}
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task IsEqualRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
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
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 1);
            //}
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task NullIsEqualStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
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
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 0);
            //}
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task NullIsEqualRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
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
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 0);
            //}
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task FoundNullIsEqualRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullBoolVal), null).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullByteVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await Assert.ThrowsAsync<RequestFailedException>(async () =>
            //{
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //});

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullCharVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await Assert.ThrowsAsync<RequestFailedException>(async () =>
            //{
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), null).Build();
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
                {
                    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                        testList.AddRange(page.Values);
                    Assert.NotNull(testList);
                    Assert.True(testList.Count == 4);
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDateTimeVal), null).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDecimalVal), null).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullDoubleVal), null).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullFloatVal), null).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullGuidVal), null).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullIntVal), null).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullLongVal), null).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullSByteVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await Assert.ThrowsAsync<RequestFailedException>(async () =>
            //{
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //});

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullShortVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await Assert.ThrowsAsync<RequestFailedException>(async () =>
            //{
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //});

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullSingleVal), null).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullStringVal), null).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 4);
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullTimeSpanVal), null).Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await Assert.ThrowsAsync<RequestFailedException>(async () =>
            //    {
            //        await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //            testList.AddRange(page.Values);
            //        Assert.NotNull(testList);
            //        Assert.True(testList.Count == 4);
            //    });
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            ////                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            ////                result = testQueryable.ToList();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 4);
            ////                result = await testQueryable.ToListAsync();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 4);
            ////            }
            ////#endif
            //if (ValidateUInt64)
            //{
            //    // UInt64
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt64Val), null).Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await Assert.ThrowsAsync<RequestFailedException>(async () =>
            //    {
            //        await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //            testList.AddRange(page.Values);
            //        Assert.NotNull(testList);
            //        Assert.True(testList.Count == 4);
            //    });
            //}

            //// UInt32
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt32Val), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await Assert.ThrowsAsync<RequestFailedException>(async () =>
            //{
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //});

            //// UInt16
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(AzureDataTablesTestClass.NullUInt16Val), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await Assert.ThrowsAsync<RequestFailedException>(async () =>
            //{
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //});
        }
    }
}