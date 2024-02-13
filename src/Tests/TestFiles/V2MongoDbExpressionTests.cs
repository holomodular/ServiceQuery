namespace ServiceQuery.Xunit
{
    public class MongoDbExpressionTests : LinqAsyncExpressionTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}