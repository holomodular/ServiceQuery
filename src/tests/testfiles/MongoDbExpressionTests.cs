namespace ServiceQuery.Xunit
{
    public class MongoDbExpressionTests : ExpressionTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}