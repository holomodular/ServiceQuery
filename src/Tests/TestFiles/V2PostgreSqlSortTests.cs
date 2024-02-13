namespace ServiceQuery.Xunit
{
    public class PostgreSqlSortTests : LinqAsyncSortTests<TestClass>
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