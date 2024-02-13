namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonBetweenTests : LinqAsyncComparisonBetweenTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}