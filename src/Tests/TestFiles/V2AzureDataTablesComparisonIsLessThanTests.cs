using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit
{
    public class AzureDataTablesComparisonIsLessThanTests : BaseTest
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
        public async Task IsLessThanStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

#if NET7_0_OR_GREATER

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.GuidVal), "11111111-1111-1111-1111-111111111111").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
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
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //        }
        }

        [Fact]
        public async Task IsLessThanRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

#if NET7_0_OR_GREATER

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.GuidVal), "11111111-1111-1111-1111-111111111111").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            Assert.Throws<Azure.RequestFailedException>(() =>
            {
                foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
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
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //        }
        }

        [Fact]
        public async Task NullIsLessThanStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
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

#if NET7_0_OR_GREATER

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullGuidVal), "11111111-1111-1111-1111-111111111111").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //testQueryable = serviceQuery.Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
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
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            testQueryable = serviceQuery.Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //        }
        }

        [Fact]
        public async Task NullIsLessThanRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;
            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
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

#if NET7_0_OR_GREATER

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullGuidVal), "11111111-1111-1111-1111-111111111111").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 0);

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
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
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            result = testQueryable.ToList();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //            result = await testQueryable.ToListAsync();
            //            Assert.NotNull(result);
            //            Assert.True(result.Count == 1);
            //        }
        }
    }
}