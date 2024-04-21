namespace ServiceQuery.Xunit
{
    [Collection("Postgres")]
    public class PostgreSqlDistinctTests : DistinctTests<TestClass>
    {
        public PostgreSqlDistinctTests()
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