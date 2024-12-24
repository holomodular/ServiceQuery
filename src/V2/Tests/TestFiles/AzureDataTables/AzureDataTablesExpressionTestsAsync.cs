using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit.Integration
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesExpressionTestsAsync : BaseTest
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
        public async Task ExpressionsStandardTests()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // No filters (match all)
            serviceQuery = ServiceQueryBuilder.New()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            Assert.Null(predicate);
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(maxPerPage: 1000, select: selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 4);

            // Begin and End (match all)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .EndExpression()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            Assert.Null(predicate);
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(maxPerPage: 1000, select: selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 4);
        }

        [Fact]
        public async Task ExpressionsAndTests()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Begin and End (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .EndExpression()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();

            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            ServiceQueryOptions optionsAllowMissingExpressions = new ServiceQueryOptions()
            {
                AllowMissingExpressions = true
            };

            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Implicit And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();

            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
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

            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Implicit And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();

            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
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

            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And Begin and End Implicit (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();

            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
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

            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And Begin and End Implicit (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();

            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
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

            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
        }

        [Fact]
        public async Task ExpressionsOrTests()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
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
        public async Task ExpressionsAndOrTests()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "1")
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "1")
                .EndExpression()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), " ")
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), " ")
                .EndExpression()
                .Build();
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
        public async Task ExpressionsAndOrGroupingTests()
        {
            await Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Begin End All
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Group before
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Group after (one)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .BeginExpression()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .EndExpression()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Group after (multiple)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "0")
                .Or()
                .BeginExpression()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .EndExpression()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Multiple Groups
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Or()
                .BeginExpression()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .EndExpression()
                .Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Multiple Groups with Begin and End
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                    .BeginExpression()
                        .IsEqual(nameof(TestClass.IntVal), "1")
                        .And()
                        .IsEqual(nameof(TestClass.StringVal), "a")
                    .EndExpression()
                    .Or()
                    .BeginExpression()
                        .IsEqual(nameof(TestClass.LongVal), "2")
                    .EndExpression()
                .EndExpression()
                .Build();
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
        public async Task ExpressionsRequestTests()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // No filters (match all)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Null(predicate);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(maxPerPage: 1000, select: selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 4);

            // Begin and End (match all)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .EndExpression()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            Assert.Null(predicate);
        }

        [Fact]
        public async Task ExpressionsAndRequestTests()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Begin and End (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .EndExpression()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 2);
            });

            ServiceQueryOptions optionsAllowMissingExpressions = new ServiceQueryOptions()
            {
                AllowMissingExpressions = true
            };

            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Implicit And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();

            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Implicit And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();

            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And Begin and End Implicit (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();

            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And Begin and End Implicit (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();

            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
                testList = new List<AzureDataTablesTestClass>();
                await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                    testList.AddRange(page.Values);
                Assert.NotNull(testList);
                Assert.True(testList.Count == 1);
            });

            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>(optionsAllowMissingExpressions);
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);
        }

        [Fact]
        public async Task ExpressionsOrRequestTests()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
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
        public async Task ExpressionsAndOrRequestTests()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "1")
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "1")
                .EndExpression()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), " ")
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), " ")
                .EndExpression()
                .Build();
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
        public async Task ExpressionsAndOrGroupingRequestTests()
        {
            await Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            AsyncPageable<AzureDataTablesTestClass> asyncpagableResult = null;

            // Begin End All
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Group before
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Group after (one)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .BeginExpression()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .EndExpression()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 1);

            // Group after (multiple)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "0")
                .Or()
                .BeginExpression()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .EndExpression()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 3);

            // Multiple Groups
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Or()
                .BeginExpression()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .EndExpression()
                .Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            asyncpagableResult = _tableClient.QueryAsync<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            testList = new List<AzureDataTablesTestClass>();
            await foreach (Page<AzureDataTablesTestClass> page in asyncpagableResult.AsPages())
                testList.AddRange(page.Values);
            Assert.NotNull(testList);
            Assert.True(testList.Count == 2);

            // Multiple Groups with Begin and End
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                    .BeginExpression()
                        .IsEqual(nameof(TestClass.IntVal), "1")
                        .And()
                        .IsEqual(nameof(TestClass.StringVal), "a")
                    .EndExpression()
                    .Or()
                    .BeginExpression()
                        .IsEqual(nameof(TestClass.LongVal), "2")
                    .EndExpression()
                .EndExpression()
                .Build();
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
        public async Task NullExpressionsAndTests()
        {
            // NOT SUPPORTED
        }

        [Fact]
        public async Task NullExpressionsOrTests()
        {
            // NOT SUPPORTED
        }

        [Fact]
        public async Task NullExpressionsAndOrTests()
        {
            //NOT SUPPORTED
        }

        [Fact]
        public async Task NullExpressionsAndOrGroupingTests()
        {
            //NOT SUPPORTED
        }
    }
}