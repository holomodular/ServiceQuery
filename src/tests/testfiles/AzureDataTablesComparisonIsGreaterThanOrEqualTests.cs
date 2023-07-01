using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit
{
    public class AzureDataTablesComparisonIsGreaterThanOrEqualTests : BaseTest
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

        [Fact]
        public async Task IsGreaterThanOrEqualOrEqualStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 3);
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 3);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 3);
            });

#if NET7_0
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 4);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 3);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task IsGreaterThanOrEqualOrEqualRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 3);
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 3);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 3);
            });

#if NET7_0
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 4);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 3);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task NullIsGreaterThanOrEqualOrEqualStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
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
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

#if NET7_0
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task NullIsGreaterThanOrEqualOrEqualRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

#if NET7_0
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
        }
    }
}