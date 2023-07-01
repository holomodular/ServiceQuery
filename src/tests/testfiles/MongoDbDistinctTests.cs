namespace ServiceQuery.Xunit
{
    public class MongoDbDistinctTests : DistinctTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}