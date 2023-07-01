namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonEndsWithTests : ComparisonEndsWithTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}