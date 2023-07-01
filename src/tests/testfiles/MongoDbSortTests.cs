namespace ServiceQuery.Xunit
{
    public class MongoDbSortTests : SortTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}