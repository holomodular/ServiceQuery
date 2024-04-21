namespace ServiceQuery.Xunit
{
    [Collection("MongoDb")]
    public class MongoDbExpressionTests : ExpressionTests<MongoDbTestClass>
    {
        public MongoDbExpressionTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }

        public override IQueryable<MongoDbTestClass> GetTestNullCopyList()
        {
            return MongoDbHelper.GetTestNullCopyList();
        }
    }
}