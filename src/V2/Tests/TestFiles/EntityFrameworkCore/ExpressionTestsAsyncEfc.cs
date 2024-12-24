using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit
{
    public class ExpressionTestsAsyncEfc : ExpressionTestsAsyncEfc<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            var list = new TestClass().GetDefaultList();
            foreach (var item in list)
                item.CopyToNullVals();
            return list.AsQueryable();
        }

        public override Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return Task.FromResult(new TestClass().GetDefaultList().AsAsyncInMemoryQueryable());
        }

        public override Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            var list = new TestClass().GetDefaultList();
            foreach (var item in list)
                item.CopyToNullVals();
            return Task.FromResult(list.AsAsyncInMemoryQueryable());
        }
    }

    public abstract class ExpressionTestsAsyncEfc<T> : BaseTest<T> where T : class
    {
        [Fact]
        public async Task SyncExpressionsStandardTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // No filters (match all)
            serviceQuery = ServiceQueryBuilder.New()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Begin and End (match all)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        private static ServiceQueryOptions optionAllowMissingExpressions = new ServiceQueryOptions { AllowMissingExpressions = true };

        [Fact]
        public async Task ExpressionsAndTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Begin and End (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .Begin()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();

            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Implicit And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Implicit And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End Implicit (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End Implicit (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task SyncExpressionsOrTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task SyncExpressionsAndOrTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "1")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

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
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), " ")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

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
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

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
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task SyncExpressionsAndOrGroupingTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

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
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

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
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

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
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

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
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

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
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

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
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task SyncExpressionsRequestTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // No filters (match all)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Begin and End (match all)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task SyncExpressionsAndRequestTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Begin and End (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Implicit And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Implicit And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End Implicit (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End Implicit (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task SyncExpressionsOrRequestTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task SyncExpressionsAndOrRequestTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "1")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

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
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), " ")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

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
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

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
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task SyncExpressionsAndOrGroupingRequestTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

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
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

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
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

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
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

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
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

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
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

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
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task SyncNullExpressionsAndTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.NullBoolVal), null)
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.NullBoolVal), null)
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.NullStringVal), null)
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Implicit And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .IsEqual(nameof(TestClass.NullStringVal), null)
                .Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // And Begin and End Implicit (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .IsEqual(nameof(TestClass.NullStringVal), null)
                .EndExpression()
                .Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            testQueryable = serviceQuery.Apply(sourceQueryable, optionAllowMissingExpressions);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task SyncNullExpressionsOrTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Or
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.NullBoolVal), null)
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.NullStringVal), null)
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.NullBoolVal), null)
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task SyncNullExpressionsAndOrTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.NullLongVal), null)
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "1")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.NullBoolVal), null)
                .Or()
                .IsEqual(nameof(TestClass.StringVal), " ")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // And Begin and End (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // And Begin and End (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Or()
                .IsEqual(nameof(TestClass.StringVal), " ")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task SyncNullExpressionsAndOrGroupingTests()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Begin End All
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Group before
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Group after (one)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .And()
                .BeginExpression()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Group after (multiple)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .Or()
                .BeginExpression()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Or()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Multiple Groups
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Or()
                .BeginExpression()
                .IsEqual(nameof(TestClass.LongVal), "2")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Multiple Groups with Begin and End
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                    .BeginExpression()
                        .IsEqual(nameof(TestClass.NullIntVal), null)
                        .And()
                        .IsEqual(nameof(TestClass.StringVal), "a")
                    .EndExpression()
                    .Or()
                    .BeginExpression()
                        .IsEqual(nameof(TestClass.LongVal), "2")
                    .EndExpression()
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }
    }
}