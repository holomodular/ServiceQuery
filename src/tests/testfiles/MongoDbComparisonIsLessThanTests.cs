namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsLessThanTests : ComparisonIsLessThanTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}