namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsLessThanTests : LinqAsyncComparisonIsLessThanTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}