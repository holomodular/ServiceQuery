namespace ServiceQuery.Xunit
{
    public class MongoDbComparisonIsNotEqualTests : LinqAsyncComparisonIsNotEqualTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}