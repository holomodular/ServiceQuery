using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit.Integration
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesSelectTestsAsync : BaseTest
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
        public async Task SelectStandardTests()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            AzureDataTablesTestClass retRecord;
            AzureDataTablesTestClass oneRecord;

            // No select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);

            // 1 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal != oneRecord.ByteArrayVal);
            Assert.True(retRecord.DateTimeOffsetVal != oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 2 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal != oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 3 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 4 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 5 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 6 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 7 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 8 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 9 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 10 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 11 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SingleVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);

            // 12 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            ////Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            ////Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            ////Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
        }

        [Fact]
        public async Task SelectRequestTests()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            AzureDataTablesTestClass retRecord;
            AzureDataTablesTestClass oneRecord;

            // No select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            ////Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            ////Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            ////Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            ////Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            ////Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            ////Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            ////Assert.True(retRecord.UInt32Val == oneRecord.UInt32Val);
            ////Assert.True(retRecord.UInt64Val == oneRecord.UInt64Val);

            // 1 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal != oneRecord.ByteArrayVal);
            //Assert.True(retRecord.ByteVal != oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal != oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal != oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 2 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal != oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal != oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal != oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 3 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal != oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal != oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 4 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal != oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 5 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 6 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 7 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 8 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 9 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 10 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 11 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 12 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 13 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 14 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 15 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 16 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 17 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 18 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 19 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Select(nameof(TestClass.UInt16Val))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 20 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Select(nameof(TestClass.UInt16Val))
                .Select(nameof(TestClass.UInt32Val))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val == oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 21 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Select(nameof(TestClass.UInt16Val))
                .Select(nameof(TestClass.UInt32Val))
                .Select(nameof(TestClass.UInt64Val))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            //Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            //Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            //Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            //Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            //Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            //Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            //Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            //Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            //Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            //Assert.True(retRecord.UInt32Val == oneRecord.UInt32Val);
            //Assert.True(retRecord.UInt64Val == oneRecord.UInt64Val);
        }

        [Fact]
        public async Task NullSelectStandardTests()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            AzureDataTablesTestClass retRecord;
            AzureDataTablesTestClass oneRecord;

            // No select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 1 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 2 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 3 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 4 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 5 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 6 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 7 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 8 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 9 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 10 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 11 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 12 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 13 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 14 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 15 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 16 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 17 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 18 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 19 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Select(nameof(TestClass.NullUInt16Val))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 20 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Select(nameof(TestClass.NullUInt16Val))
                .Select(nameof(TestClass.NullUInt32Val))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 21 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Select(nameof(TestClass.NullUInt16Val))
                .Select(nameof(TestClass.NullUInt32Val))
                .Select(nameof(TestClass.NullUInt64Val))
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);
        }

        [Fact]
        public async Task NullSelectRequestTests()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            AzureDataTablesTestClass retRecord;
            AzureDataTablesTestClass oneRecord;

            // No select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 1 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 2 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 3 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 4 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 5 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 6 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 7 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 8 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 9 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 10 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 11 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 12 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 13 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 14 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 15 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 16 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 17 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
            retRecord = testList[0];
            oneRecord = AzureDataTablesTestClass.GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            //Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            //Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            //Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            //Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            //Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            //Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            //Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            //Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);
        }
    }
}