namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsGreaterThanTests : ComparisonIsGreaterThanTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}