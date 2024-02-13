namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsGreaterThanOrEqualTests : LinqAsyncComparisonIsGreaterThanOrEqualTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}