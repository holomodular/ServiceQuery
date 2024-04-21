using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesComparisonIsNullTests : BaseTest
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
        public async Task IsNullStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.BoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.ByteVal)).Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.CharVal)).Build();
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
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                    testList = new List<AzureDataTablesTestClass>();
                    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                        testList.AddRange(page.Values);
                    Assert.NotNull(testList);
                    Assert.True(testList.Count == 0);
                });
            }

            // DateTime
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.DecimalVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.DoubleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.FloatVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.IntVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.LongVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.SByteVal)).Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.ShortVal)).Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.SingleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.StringVal)).Build();
            Assert.Throws<RequestFailedException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.TimeSpanVal)).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 0);
            //}
            //if (ValidateUInt64)
            //{
            //    // UInt64
            //    serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.UInt64Val)).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 0);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.UInt32Val)).Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// UInt16
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.UInt16Val)).Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);
        }

        [Fact]
        public async Task IsNullRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.BoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.ByteVal)).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.CharVal)).Build();
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
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                    testList = new List<AzureDataTablesTestClass>();
                    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                        testList.AddRange(page.Values);
                    Assert.NotNull(testList);
                    Assert.True(testList.Count == 0);
                });
            }

            // DateTime
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.DecimalVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.DoubleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.FloatVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.IntVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.LongVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.SByteVal)).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.ShortVal)).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.SingleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.StringVal)).Build();
            Assert.Throws<RequestFailedException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.TimeSpanVal)).Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 0);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.UInt128Val)).Build();
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
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.UInt64Val)).Build();
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 0);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.UInt32Val)).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// UInt16
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.UInt16Val)).Build();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);
        }

        [Fact]
        public async Task NullIsNullStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullBoolVal)).Build();
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
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullByteVal)).Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            // Char
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullCharVal)).Build();
            // predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            // selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            // pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            // testList = new List<AzureDataTablesTestClass>();
            // foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //     testList.AddRange(page.Values);
            // Assert.NotNull(testList);
            // Assert.True(testList.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal)).Build();
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
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDateTimeVal)).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDecimalVal)).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDoubleVal)).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullFloatVal)).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullGuidVal)).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullIntVal)).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullLongVal)).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // SByte
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullSByteVal)).Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullShortVal)).Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullSingleVal)).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullStringVal)).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullTimeSpanVal)).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 0);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt128Val)).Build();
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
            //    serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt64Val)).Build();
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //    selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //    pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //    testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 0);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt32Val)).Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);

            //// UInt16
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt16Val)).Build();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 0);
        }

        [Fact]
        public async Task NullIsNullRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullBoolVal)).Build();
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
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullByteVal)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullCharVal)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal)).Build();
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
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDateTimeVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDecimalVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDoubleVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullFloatVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullGuidVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullIntVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullLongVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullSByteVal)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullShortVal)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullSingleVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullStringVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullTimeSpanVal)).Build();
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 4);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 4);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt128Val)).Build();
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
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt64Val)).Build();
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 4);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 4);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt32Val)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// UInt16
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt16Val)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task FoundNullIsEqualRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullBoolVal)).Build();
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
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullByteVal)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullCharVal)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = AzureDataTablesTestClass.GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal)).Build();
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
            var tempDateTime = AzureDataTablesTestClass.GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDateTimeVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDecimalVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullDoubleVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullFloatVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullGuidVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullIntVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullLongVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullSByteVal)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullShortVal)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullSingleVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullStringVal)).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 0);
            });

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullTimeSpanVal)).Build();
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 4);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 4);
            //}
            ////#if NET7_0
            ////            if (ValidateUInt128)
            ////            {
            ////                // UInt128
            ////                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt128Val)).Build();
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
            //    serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt64Val)).Build();
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    result = testQueryable.ToList();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 4);
            //    result = await testQueryable.ToListAsync();
            //    Assert.NotNull(result);
            //    Assert.True(result.Count == 4);
            //}

            //// UInt32
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt32Val)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            //// UInt16
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(AzureDataTablesTestClass.NullUInt16Val)).Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
        }
    }
}