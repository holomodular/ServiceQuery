namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonStartsWithTests : LinqAsyncComparisonStartsWithTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}