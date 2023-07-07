namespace ServiceQuery.Xunit
{
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
    }
}