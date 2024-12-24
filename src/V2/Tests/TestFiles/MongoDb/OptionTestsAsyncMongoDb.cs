using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ServiceQuery.Xunit
{
    public class OptionTestsAsyncMongoDb : OptionTestsAsyncMongoDb<TestClass>
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

    public abstract class OptionTestsAsyncMongoDb<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public async Task PropertyNameSensitivity()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            var req = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Build();
            var resp = req.Execute(sourceQueryable);
            Assert.True(resp.List.Count == 1);

            req = ServiceQueryRequestBuilder.New().IsEqual("intval", "1").Build();

            Assert.Throws<ServiceQueryException>(() =>
            {
                resp = req.Execute(sourceQueryable);
            });
        }

        [Fact]
        public async Task NullOptions()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;

            var req = ServiceQueryRequestBuilder.New().Select(nameof(TestClass.IntVal)).IsEqual(nameof(TestClass.IntVal), "1").Build();

            var sq = req.GetServiceQuery();
            var q2 = sq.Apply(sourceQueryable);

            ServiceQueryOptions serviceQueryOptions = new ServiceQueryOptions();
            var qo = sq.BuildOrderByExpression(sourceQueryable, serviceQueryOptions);
            var qs = sq.BuildSelectExpression<TestClass>(serviceQueryOptions);
            var qw = sq.BuildWhereExpression<TestClass>(serviceQueryOptions);
            var ea = sq.ExecuteAggregate(sourceQueryable, serviceQueryOptions);
            var sp = sq.GetSelectProperties<TestClass>(serviceQueryOptions);

            var resp = req.Execute(sourceQueryable);
            Assert.True(resp.List.Count == 1);

            req = ServiceQueryRequestBuilder.New().IsEqual("intval", "1").Build();

            Assert.Throws<ServiceQueryException>(() =>
            {
                resp = req.Execute(sourceQueryable);
            });
        }
    }
}