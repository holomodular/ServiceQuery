using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Azure;
using ServiceQuery;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit.Integration
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesComparisonIsNotInSetTests : BaseTest
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
        public async Task IsNotInSetStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.BoolVal), "true").Build();
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

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ByteVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.CharVal), "b").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
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

            //// Decimal
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DecimalVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DoubleVal), "1").Build();
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

            //// Float
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.FloatVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
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

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.IntVal), "1").Build();
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

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.LongVal), "1").Build();
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

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SByteVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ShortVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// Single
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SingleVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.StringVal), "a").Build();
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

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 3);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 3);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt128Val), "1").Build();
            ////                testQueryable = serviceQuery.Apply(sourceQueryable);
            ////                result = testQueryable.ToList();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 3);
            ////                result = await testQueryable.ToListAsync();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 3);
            ////            }
            ////#endif
            //if (ValidateUInt64)
            //{
            //    // UInt64
            //    serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt64Val), "1").Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 3);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 3);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt32Val), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// UInt16
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt16Val), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
        }

        [Fact]
        public async Task IsNotInSetTwo()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.BoolVal), "true", "false").Build();
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
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ByteVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.CharVal), "a", "b").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                var tempDateTimeOffset2 = new TestClass().GetDefault2Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
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
            }

            // DateTime
            var tempDateTime1 = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            var tempDateTime2 = new TestClass().GetDefault2Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
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

            //// Decimal
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DecimalVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DoubleVal), "1", "2").Build();
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

            //// Float
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.FloatVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // Guid
            var tempGuid = new TestClass().GetDefault1Record(new TestClass()).GuidVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempGuid.ToString()).Build();
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

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.IntVal), "1", "2").Build();
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

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.LongVal), "1", "2").Build();
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

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SByteVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ShortVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// Single
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SingleVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.StringVal), "a", "b").Build();
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

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString(), TimeSpan.FromMilliseconds(2).ToString()).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 2);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 2);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt128Val), "1", "2").Build();
            ////                testQueryable = serviceQuery.Apply(sourceQueryable);
            ////                result = testQueryable.ToList();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 2);
            ////                result = await testQueryable.ToListAsync();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 2);
            ////            }
            ////#endif
            //if (ValidateUInt64)
            //{
            //    // UInt64
            //    serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt64Val), "1", "2").Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 2);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 2);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt32Val), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// UInt16
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt16Val), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
        }

        [Fact]
        public async Task IsNotInSetRequestTest()
        {
            Startup();
            ServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.BoolVal), "true").Build();
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

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.ByteVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.CharVal), "b").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
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

            //// Decimal
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.DecimalVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.DoubleVal), "1").Build();
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

            //// Float
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.FloatVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
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

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.IntVal), "1").Build();
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

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.LongVal), "1").Build();
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

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.SByteVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.ShortVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// Single
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.SingleVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.StringVal), "a").Build();
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

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 3);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 3);
            //}
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#endif
            //if (ValidateUInt64)
            //{
            //    // UInt64
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.UInt64Val), "1").Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 3);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 3);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.UInt32Val), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);

            //// UInt16
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.UInt16Val), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 3);
        }

        [Fact]
        public async Task IsNotInSetTwoRequestTests()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.BoolVal), "true", "false").Build();
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
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ByteVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.CharVal), "a", "b").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                var tempDateTimeOffset2 = new TestClass().GetDefault2Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
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
            }

            // DateTime
            var tempDateTime1 = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            var tempDateTime2 = new TestClass().GetDefault2Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
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

            //// Decimal
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DecimalVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DoubleVal), "1", "2").Build();
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

            //// Float
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.FloatVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // Guid
            var tempGuid = new TestClass().GetDefault1Record(new TestClass()).GuidVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempGuid.ToString()).Build();
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

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.IntVal), "1", "2").Build();
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

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.LongVal), "1", "2").Build();
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

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SByteVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ShortVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// Single
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SingleVal), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.StringVal), "a", "b").Build();
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

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString(), TimeSpan.FromMilliseconds(2).ToString()).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 2);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 2);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt128Val), "1", "2").Build();
            ////                testQueryable = serviceQuery.Apply(sourceQueryable);
            ////                result = testQueryable.ToList();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 2);
            ////                result = await testQueryable.ToListAsync();
            ////                Assert.NotNull(result);
            ////                Assert.True(result.Count == 2);
            ////            }
            ////#endif
            //if (ValidateUInt64)
            //{
            //    // UInt64
            //    serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt64Val), "1", "2").Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 2);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 2);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt32Val), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);

            //// UInt16
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt16Val), "1", "2").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 2);
        }

        [Fact]
        public async Task NullIsNotInSetStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullBoolVal), "true").Build();
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
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullByteVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullCharVal), "b").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
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

            //// Decimal
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDoubleVal), "1").Build();
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

            //// Float
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullFloatVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
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

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullIntVal), "1").Build();
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

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullLongVal), "1").Build();
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

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullSByteVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullShortVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            //// Single
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullSingleVal), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullStringVal), "a").Build();
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

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt128Val), "1").Build();
            ////                testQueryable = serviceQuery.Apply(sourceQueryable);
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
            //    serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt64Val), "1").Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt32Val), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            //// UInt16
            //serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt16Val), "1").Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
        }

        [Fact]
        public async Task NullIsNotInSetRequestTest()
        {
            Startup();
            ServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullBoolVal), "true").Build();
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
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullByteVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullCharVal), "b").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
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

            //// Decimal
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDoubleVal), "1").Build();
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

            //// Float
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullFloatVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
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

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullIntVal), "1").Build();
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

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullLongVal), "1").Build();
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

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullSByteVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullShortVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            //// Single
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullSingleVal), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullStringVal), "a").Build();
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

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt128Val), "1").Build();
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
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt64Val), "1").Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 4);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt32Val), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);

            //// UInt16
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt16Val), "1").Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 4);
        }

        [Fact]
        public void NullIsNotInSetRequestNoneTest()
        {
            //Startup();
            //ServiceQueryRequest serviceQuery;
            //List<string> selectProperties;
            //Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            //List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            //Pageable<AzureDataTablesTestClass> pagableResult = null;
            //AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            //// Boolean
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullBoolVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// ByteArray
            ////var tempbyteArray = new byte[1] { 1 };
            ////serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            ////testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            ////result = testQueryable.ToList();
            ////Assert.NotNull(result);
            ////Assert.True(result.Count == 0);
            ////result = await testQueryable.ToListAsync();
            ////Assert.NotNull(result);
            ////Assert.True(result.Count == 0);

            ////// Byte
            ////serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullByteVal), null).Build();
            ////predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);
            ////asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);

            ////// Char
            ////serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullCharVal), null).Build();
            ////predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);
            ////asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);

            //if (ValidateDateTimeOffset)
            //{
            //    // DateTimeOffset
            //    var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeOffsetVal), null).Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 0);
            //    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 0);
            //}

            //// DateTime
            //var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            ////// Decimal
            ////serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDecimalVal), null).Build();
            ////predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);
            ////asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);

            //// Double
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDoubleVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            ////// Float
            ////serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullFloatVal), null).Build();
            ////predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);
            ////asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);

            //// Guid
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullGuidVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Int
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullIntVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Long
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullLongVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            ////// SByte
            ////serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullSByteVal), null).Build();
            ////predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);
            ////asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);

            ////// Short
            ////serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullShortVal), null).Build();
            ////predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);
            ////asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);

            ////// Single
            ////serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullSingleVal), null).Build();
            ////predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);
            ////asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);

            //// String
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullStringVal), null).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);
            //asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            ////if (ValidateTimeSpan)
            ////{
            ////    // TimeSpan
            ////    serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullTimeSpanVal), null).Build();
            ////    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////    testList = new List<AzureDataTablesTestClass>();
            ////    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////        testList.AddRange(page.Values);
            ////    Assert.NotNull(testList);
            ////    Assert.True(testList.Count == 0);
            ////    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////    testList = new List<AzureDataTablesTestClass>();
            ////    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////        testList.AddRange(page.Values);
            ////    Assert.NotNull(testList);
            ////    Assert.True(testList.Count == 0);
            ////}
            //////#if NET7_0
            //////            if (ValidateUInt128)
            //////            {
            //////                // UInt128
            //////                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt128Val), null).Build();
            //////                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //////                result = testQueryable.ToList();
            //////                Assert.NotNull(result);
            //////                Assert.True(result.Count == 0);
            //////                result = await testQueryable.ToListAsync();
            //////                Assert.NotNull(result);
            //////                Assert.True(result.Count == 0);
            //////            }
            //////#endif
            ////if (ValidateUInt64)
            ////{
            ////    // UInt64
            ////    serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt64Val), null).Build();
            ////    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////    testList = new List<AzureDataTablesTestClass>();
            ////    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////        testList.AddRange(page.Values);
            ////    Assert.NotNull(testList);
            ////    Assert.True(testList.Count == 0);
            ////    asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////    testList = new List<AzureDataTablesTestClass>();
            ////    await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////        testList.AddRange(page.Values);
            ////    Assert.NotNull(testList);
            ////    Assert.True(testList.Count == 0);
            ////}

            ////// UInt32
            ////serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt32Val), null).Build();
            ////predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);
            ////asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);

            ////// UInt16
            ////serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt16Val), null).Build();
            ////predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            ////selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            ////pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);
            ////asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            ////testList = new List<AzureDataTablesTestClass>();
            ////await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
            ////    testList.AddRange(page.Values);
            ////Assert.NotNull(testList);
            ////Assert.True(testList.Count == 0);
        }
    }
}