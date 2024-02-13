namespace ServiceQuery.Xunit
{
    public class MongoDbDistinctTests : LinqAsyncDistinctTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}