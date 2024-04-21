using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesComparisonIsNotEqualTests : BaseTest
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
        public async Task IsNotEqualStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
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

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                testList = new List<AzureDataTablesTestClass>();
            //                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //                    testList.AddRange(page.Values);
            //                Assert.NotNull(testList);
            //                Assert.True(testList.Count == 3);
            //            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
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
            //                serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task IsNotEqualRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                testList = new List<AzureDataTablesTestClass>();
            //                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //                    testList.AddRange(page.Values);
            //                Assert.NotNull(testList);
            //                Assert.True(testList.Count == 3);
            //            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
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
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task NullIsNotEqualStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
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

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                testList = new List<AzureDataTablesTestClass>();
            //                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //                    testList.AddRange(page.Values);
            //                Assert.NotNull(testList);
            //                Assert.True(testList.Count == 0);
            //            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
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
            //                serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task NullIsNotEqualRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
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

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
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

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                testList = new List<AzureDataTablesTestClass>();
            //                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //                    testList.AddRange(page.Values);
            //                Assert.NotNull(testList);
            //                Assert.True(testList.Count == 0);
            //            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
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
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 3);

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsNotEqual(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
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