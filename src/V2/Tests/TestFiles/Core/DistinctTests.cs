namespace ServiceQuery.Xunit
{
    public class DistinctTests : DistinctTests<TestClass>
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
            return Task.FromResult(GetTestList());
        }

        public override Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return Task.FromResult(GetTestNullCopyList());
        }

        [Fact]
        public override void SyncDistinctStandardTest()
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public override void SyncDistinctRequestTest()
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }
    }

    public abstract class DistinctTests<T> : BaseTest<T> where T : class
    {
        [Fact]
        public virtual void SyncDistinctStandardTest()
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public virtual void SyncDistinctRequestTest()
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
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }
    }
}