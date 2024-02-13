namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonContainsTests : LinqAsyncComparisonContainsTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}