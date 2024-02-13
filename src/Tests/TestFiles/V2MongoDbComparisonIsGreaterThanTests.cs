namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsGreaterThanTests : LinqAsyncComparisonIsGreaterThanTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}