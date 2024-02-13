﻿namespace ServiceQuery.Xunit
{
    public class SqlLiteSelectTests : LinqAsyncSelectTests<TestClass>
    {
        public SqlLiteSelectTests()
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