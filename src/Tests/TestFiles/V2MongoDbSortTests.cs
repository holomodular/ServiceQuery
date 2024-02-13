namespace ServiceQuery.Xunit
{
    public class MongoDbSortTests : LinqAsyncSortTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}