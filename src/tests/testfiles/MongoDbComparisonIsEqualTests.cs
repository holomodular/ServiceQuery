namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsEqualTests : ComparisonIsEqualTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}