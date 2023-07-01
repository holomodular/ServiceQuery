namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonStartsWithTests : ComparisonStartsWithTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}