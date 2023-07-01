namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsGreaterThanOrEqualTests : ComparisonIsGreaterThanOrEqualTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}