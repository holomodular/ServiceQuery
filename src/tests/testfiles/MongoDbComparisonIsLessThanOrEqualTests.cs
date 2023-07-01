namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsLessThanOrEqualTests : ComparisonIsLessThanOrEqualTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}