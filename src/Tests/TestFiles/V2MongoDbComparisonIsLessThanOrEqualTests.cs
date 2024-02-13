namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsLessThanOrEqualTests : LinqAsyncComparisonIsLessThanOrEqualTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}