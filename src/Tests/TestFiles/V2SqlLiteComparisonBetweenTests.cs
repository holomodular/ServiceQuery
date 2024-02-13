﻿namespace ServiceQuery.Xunit
{
    public class SqlLiteComparisonBetweenTests : LinqAsyncComparisonBetweenTests<TestClass>
    {
        public SqlLiteComparisonBetweenTests()
        {
            ValidateDateTimeOffset = false;
            ValidateTimeSpan = false;
            ValidateUInt128 = false;
            ValidateUInt64 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlLiteHelper.GetTestList();
        }
    }
}