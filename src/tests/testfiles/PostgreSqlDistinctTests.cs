namespace ServiceQuery.Xunit
{
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
    }
}