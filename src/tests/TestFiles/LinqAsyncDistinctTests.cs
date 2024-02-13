namespace ServiceQuery.Xunit
{
    public class LinqAsyncDistinctTests : LinqAsyncDistinctTests<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }

        [Fact]
        public override async Task DistinctStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<TestClass> testQueryable;
            IServiceQuery serviceQuery;
            List<TestClass> result;

            // No Equals() and GetHashcode() overrides on class
            serviceQuery = ServiceQueryBuilder.New().Select(nameof(TestClass.SharedParentKey)).Distinct().Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public override async Task DistinctRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<TestClass> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<TestClass> result;

            // No Equals() and GetHashcode() overrides on class
            serviceQuery = ServiceQueryRequestBuilder.New().Select(nameof(TestClass.SharedParentKey)).Distinct().Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }
    }

    public abstract class LinqAsyncDistinctTests<T> : BaseTest<T> where T : class
    {
        [Fact]
        public virtual async Task DistinctStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // No Equals() and GetHashcode() overrides on class
            serviceQuery = ServiceQueryBuilder.New().Select(nameof(TestClass.SharedParentKey)).Distinct().Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public virtual async Task DistinctRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // No Equals() and GetHashcode() overrides on class
            serviceQuery = ServiceQueryRequestBuilder.New().Select(nameof(TestClass.SharedParentKey)).Distinct().Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }
    }
}