namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsEqualTests : LinqAsyncComparisonIsEqualTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}