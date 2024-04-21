namespace ServiceQuery.Xunit
{
    [Collection("Postgres")]
    public class PostgreSqlSelectTests : SelectTests<TestClass>
    {
        public PostgreSqlSelectTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return PostgreSqlHelper.GetTestNullCopyList();
        }
    }
}