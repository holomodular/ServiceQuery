namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonEndsWithTests : LinqAsyncComparisonEndsWithTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}