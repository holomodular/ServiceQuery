namespace ServiceQuery.Xunit
{
    public class SqlServerDistinctTests : LinqAsyncDistinctTests<TestClass>
    {
        public SqlServerDistinctTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}