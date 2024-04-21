namespace ServiceQuery.Xunit
{
    [Collection("Postgres")]
    public class PostgreSqlSortTests : SortTests<TestClass>
    {
        public PostgreSqlSortTests()
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