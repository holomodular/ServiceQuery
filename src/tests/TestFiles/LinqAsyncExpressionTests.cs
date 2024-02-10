#if NET8_0_OR_GREATER

#else
using System.Data.Entity;
#endif

namespace ServiceQuery.Xunit
{
    public class LinqAsyncExpressionTests : ExpressionTests<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }
    }

    public abstract class LinqAsyncExpressionTests<T> : BaseTest<T> where T : class
    {
        [Fact]
        public async Task ExpressionsStandardTests()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // No filters (match all)
            serviceQuery = ServiceQueryBuilder.New()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Begin and End (match all)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task ExpressionsAndTests()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Begin and End (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .Begin()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .End()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Implicit And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Implicit And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End Implicit (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End Implicit (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task ExpressionsOrTests()
        {
            var sourceQueryable = GetTestList();
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task ExpressionsAndOrTests()
        {
            var sourceQueryable = GetTestList();
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task ExpressionsAndOrGroupingTests()
        {
            var sourceQueryable = GetTestList();
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task ExpressionsRequestTests()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // No filters (match all)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Begin and End (match all)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task ExpressionsAndRequestTests()
        {
            var sourceQueryable = GetTestList();
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Implicit And (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Implicit And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End Implicit (match one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.StringVal), "a")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And Begin and End Implicit (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task ExpressionsOrRequestTests()
        {
            var sourceQueryable = GetTestList();
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // And (match more than one record)
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .IsEqual(nameof(TestClass.BoolVal), "true")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task ExpressionsAndOrRequestTests()
        {
            var sourceQueryable = GetTestList();
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task ExpressionsAndOrGroupingRequestTests()
        {
            var sourceQueryable = GetTestList();
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task NullExpressionsAndTests()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.NullBoolVal), null)
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // And (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .IsEqual(nameof(TestClass.NullBoolVal), null)
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Implicit And (match one record)
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .IsEqual(nameof(TestClass.NullStringVal), null)
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // And Begin and End Implicit (match more than one record)
            serviceQuery = ServiceQueryBuilder.New()
                .BeginExpression()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .IsEqual(nameof(TestClass.NullStringVal), null)
                .EndExpression()
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task NullExpressionsOrTests()
        {
            var sourceQueryable = GetTestList();
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task NullExpressionsAndOrTests()
        {
            var sourceQueryable = GetTestList();
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task NullExpressionsAndOrGroupingTests()
        {
            var sourceQueryable = GetTestList();
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }
    }
}