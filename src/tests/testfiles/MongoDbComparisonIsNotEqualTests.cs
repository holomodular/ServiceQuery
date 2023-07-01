namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsNotEqualTests : ComparisonIsNotEqualTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}