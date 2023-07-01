using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit
{
    public class AzureDataTablesComparisonBetweenTests : BaseTest
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
        public async Task BetweenStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.BoolVal), "false", "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

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

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.ByteVal), "0", "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.CharVal), "a", "b").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new TestClass().GetDefault0Record().DateTimeOffsetVal;
                var tempDateTimeOffset2 = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            }

            // DateTime
            var tempDateTime1 = new TestClass().GetDefault0Record().DateTimeVal;
            var tempDateTime2 = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.DateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.DecimalVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.DoubleVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
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
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.FloatVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            // Guid
            var tempg = new TestClass().GetDefault1Record();
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempg.GuidVal.ToString()).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.IntVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.LongVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.SByteVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.ShortVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.SingleVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.StringVal), "a", "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString(), TimeSpan.FromMilliseconds(2).ToString()).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 2);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.UInt128Val), "1").Build();
            ////                testQueryable = serviceQuery.Apply(sourceQueryable);
            ////                result = testQueryable.ToList();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 1);
            ////                result = await testQueryable.ToListAsync();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 1);
            ////            }
            ////#endif
            //if (ValidateUInt64)
            //{
            //    // UInt64
            //    serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.UInt64Val), "1", "2").Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 2);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.UInt32Val), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// UInt16
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.UInt16Val), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
        }

        [Fact]
        public async Task BetweenRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.BoolVal), "false", "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.ByteVal), "0", "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.CharVal), "a", "b").Build();
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
                var tempDateTimeOffset1 = new TestClass().GetDefault0Record().DateTimeOffsetVal;
                var tempDateTimeOffset2 = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            }

            // DateTime
            var tempDateTime1 = new TestClass().GetDefault0Record().DateTimeVal;
            var tempDateTime2 = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.DateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.DecimalVal), "1", "2").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.DoubleVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
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
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.FloatVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 3);
            });

            // Guid
#if NET6_0
            var tempg = new TestClass().GetDefault1Record();
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempg.GuidVal.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif
#if NET7_0
            var tempg = new TestClass().GetDefault1Record();
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempg.GuidVal.ToString()).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);
#endif

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.IntVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.LongVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.SByteVal), "1", "2").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.ShortVal), "1", "2").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.SingleVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.StringVal), "a", "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString(), TimeSpan.FromMilliseconds(2).ToString()).Build();
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.UInt128Val), "1").Build();
            ////                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            ////                result = testQueryable.ToList();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 1);
            ////                result = await testQueryable.ToListAsync();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 1);
            ////            }
            ////#endif
            //if (ValidateUInt64)
            //{
            //    // UInt64
            //    serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.UInt64Val), "1", "2").Build();
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 2);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.UInt32Val), "1", "2").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);

            //// UInt16
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.UInt16Val), "1", "2").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task NullBetweenStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullBoolVal), "false", "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullByteVal), "0", "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullCharVal), "a", "b").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new TestClass().GetDefault0Record().DateTimeOffsetVal;
                var tempDateTimeOffset2 = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
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
            var tempDateTime1 = new TestClass().GetDefault0Record().DateTimeVal;
            var tempDateTime2 = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullDateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullDecimalVal), "1", "2").Build();
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
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullDoubleVal), "1", "2").Build();
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
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullFloatVal), "1", "2").Build();
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

            // Guid
#if NET6_0
            var tempg = new TestClass().GetDefault1Record();
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullGuidVal), Guid.Empty.ToString(), tempg.GuidVal.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif
#if NET7_0
            var tempg = new TestClass().GetDefault1Record();
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullGuidVal), Guid.Empty.ToString(), tempg.GuidVal.ToString()).Build();
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
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullIntVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullLongVal), "1", "2").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // SByte
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullSByteVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullShortVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullSingleVal), "1", "2").Build();
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
            serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullStringVal), "a", "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString(), TimeSpan.FromMilliseconds(2).ToString()).Build();
            //    testQueryable = serviceQuery.Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 0);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 0);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt128Val), "1").Build();
            ////                testQueryable = serviceQuery.Apply(sourceQueryable);
            ////                result = testQueryable.ToList();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 1);
            ////                result = await testQueryable.ToListAsync();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 1);
            ////            }
            ////#endif
            //if (ValidateUInt64)
            //{
            //    // UInt64
            //    serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullUInt64Val), "1", "2").Build();
            //    testQueryable = serviceQuery.Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 0);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 0);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullUInt32Val), "1", "2").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            //// UInt16
            //serviceQuery = ServiceQueryBuilder.New().Between(nameof(TestClass.NullUInt16Val), "1", "2").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task NullBetweenRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullBoolVal), "false", "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullByteVal), "0", "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullCharVal), "a", "b").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new TestClass().GetDefault0Record().DateTimeOffsetVal;
                var tempDateTimeOffset2 = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
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
            var tempDateTime1 = new TestClass().GetDefault0Record().DateTimeVal;
            var tempDateTime2 = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullDateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullDecimalVal), "1", "2").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullDoubleVal), "1", "2").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullFloatVal), "1", "2").Build();
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

            // Guid
#if NET6_0
            var tempg = new TestClass().GetDefault1Record();
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullGuidVal), Guid.Empty.ToString(), tempg.GuidVal.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif
#if NET7_0
            var tempg = new TestClass().GetDefault1Record();
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullGuidVal), Guid.Empty.ToString(), tempg.GuidVal.ToString()).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullIntVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullLongVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullSByteVal), "1", "2").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullShortVal), "1", "2").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullSingleVal), "1", "2").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullStringVal), "a", "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString(), TimeSpan.FromMilliseconds(2).ToString()).Build();
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 0);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 0);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt128Val), "1").Build();
            ////                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            ////                result = testQueryable.ToList();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 1);
            ////                result = await testQueryable.ToListAsync();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 1);
            ////            }
            ////#endif
            //if (ValidateUInt64)
            //{
            //    // UInt64
            //    serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullUInt64Val), "1", "2").Build();
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 0);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 0);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullUInt32Val), "1", "2").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            //// UInt16
            //serviceQuery = ServiceQueryRequestBuilder.New().Between(nameof(TestClass.NullUInt16Val), "1", "2").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
        }
    }
}