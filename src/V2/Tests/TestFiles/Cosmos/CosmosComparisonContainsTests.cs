﻿namespace ServiceQuery.Xunit.Integration
{
    [Collection("Cosmos")]
    public class CosmosComparisonContainsTests : ComparisonContainsTests<TestClass>
    {
        public CosmosComparisonContainsTests()
        {
            ValidateTimeOnly = false;
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return CosmosHelper.GetTestNullCopyList();
        }

        public override async Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return await CosmosHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return await CosmosHelper.GetTestNullCopyListAsync();
        }
    }
}