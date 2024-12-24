using MongoDB.Driver;

namespace ServiceQuery.Xunit
{
    public class AggregateCountTestsAsyncMongoDb : AggregateCountTestsAsyncMongoDb<TestClass>
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
            return Task.FromResult<IQueryable<TestClass>>(null);
        }

        public override Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return Task.FromResult<IQueryable<TestClass>>(null);
        }
    }

    public abstract class AggregateCountTestsAsyncMongoDb<T> : BaseTest<T> where T : class
    {
        [Fact]
        public async Task CountStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IServiceQuery serviceQuery;
            double? result;

            serviceQuery = ServiceQueryBuilder.New().Count().Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 4);
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 4);

            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Count().Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 1);
        }

        [Fact]
        public async Task CountRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IServiceQueryRequest serviceQuery;
            double? result;

            serviceQuery = ServiceQueryRequestBuilder.New().Count().Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 4);
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 4);

            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Count().Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 1);
        }
    }
}