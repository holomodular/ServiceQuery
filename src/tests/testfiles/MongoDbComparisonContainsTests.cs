namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonContainsTests : ComparisonContainsTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}