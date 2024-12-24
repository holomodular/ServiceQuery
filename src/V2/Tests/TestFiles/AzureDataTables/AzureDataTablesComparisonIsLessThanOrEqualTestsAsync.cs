using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit.Integration
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesComparisonIsLessThanOrEqualTestsAsync : BaseTest
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
        public async Task IsLessThanOrEqualStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
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

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.GuidVal), "11111111-1111-1111-1111-111111111111").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    testQueryable = serviceQuery.Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //}

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task IsLessThanOrEqualRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
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

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.GuidVal), "11111111-1111-1111-1111-111111111111").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
            {
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //}

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task NullIsLessThanOrEqualStandardTest()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
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

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
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

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullGuidVal), "11111111-1111-1111-1111-111111111111").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    testQueryable = serviceQuery.Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //}

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task NullIsLessThanOrEqualRequestTest()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
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

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
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

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullGuidVal), "11111111-1111-1111-1111-111111111111").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //}

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 2);
        }
    }
}