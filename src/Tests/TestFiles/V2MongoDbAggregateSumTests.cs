namespace ServiceQuery.Xunit
{
    public class MongoDbAggregateSumTests : LinqAsyncAggregateSumTests<MongoDbTestClass>
    {
        public MongoDbAggregateSumTests()
        {
            ValidateDecimal = false;
        }

        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}