namespace ServiceQuery.Xunit
{
    public class SqlLiteSortTests : LinqAsyncSortTests<TestClass>
    {
        public SqlLiteSortTests()
        {
            ValidateDateTimeOffset = false;
            ValidateTimeSpan = false;
            ValidateUInt128 = false;
            ValidateUInt64 = false;
            ValidateDecimal = false; //NotSupported
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlLiteHelper.GetTestList();
        }
    }
}