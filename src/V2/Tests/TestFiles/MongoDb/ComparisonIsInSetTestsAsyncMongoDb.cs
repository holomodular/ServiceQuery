﻿using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ServiceQuery.Xunit
{
    public class ComparisonIsInSetTestsAsyncMongoDb : ComparisonIsInSetTestsAsyncMongoDb<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            var list = new TestClass().GetDefaultList();
            foreach (var item in list)
                item.CopyToNullVals();
            return list.AsQueryable();
        }

        public override Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return Task.FromResult<IQueryable<TestClass>>(null);
        }

        public override Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return Task.FromResult<IQueryable<TestClass>>(null);
        }
    }

    public abstract class ComparisonIsInSetTestsAsyncMongoDb<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public async Task SyncIsInSetStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.BoolVal), "true").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.StringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.TimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.UInt128Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task SyncIsInSetTwo()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.BoolVal), "true", "false").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.ByteVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.CharVal), "a", "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                var tempDateTimeOffset2 = new T().GetDefault2Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly1 = new T().GetDefault1Record(new T()).DateOnlyVal;
            var tempDateOnly2 = new T().GetDefault2Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DateOnlyVal), tempDateOnly1.ToString("o"), tempDateOnly2.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#endif

            // DateTime
            var tempDateTime1 = new T().GetDefault1Record(new T()).DateTimeVal;
            var tempDateTime2 = new T().GetDefault2Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DecimalVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.DoubleVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.FloatVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Guid
            var tempGuid = new T().GetDefault1Record(new T()).GuidVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempGuid.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.IntVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.LongVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.SByteVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.ShortVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.SingleVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.StringVal), "a", "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly1 = new T().GetDefault1Record(new T()).TimeOnlyVal;
                var tempTimeOnly2 = new T().GetDefault2Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.TimeOnlyVal), tempTimeOnly1.ToString("o"), tempTimeOnly2.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString(), TimeSpan.FromMilliseconds(2).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.UInt128Val), "1", "2").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.UInt64Val), "1", "2").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.UInt32Val), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.UInt16Val), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task SyncIsInSetRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.BoolVal), "true").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.StringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.TimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.UInt128Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task SyncIsInSetTwoRequest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.BoolVal), "true", "false").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.ByteVal), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.CharVal), "a", "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                var tempDateTimeOffset2 = new T().GetDefault2Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly1 = new T().GetDefault1Record(new T()).DateOnlyVal;
            var tempDateOnly2 = new T().GetDefault2Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DateOnlyVal), tempDateOnly1.ToString("o"), tempDateOnly2.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#endif

            // DateTime
            var tempDateTime1 = new T().GetDefault1Record(new T()).DateTimeVal;
            var tempDateTime2 = new T().GetDefault2Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DecimalVal), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.DoubleVal), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.FloatVal), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Guid
            var tempGuid = new T().GetDefault1Record(new T()).GuidVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempGuid.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.IntVal), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.LongVal), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.SByteVal), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.ShortVal), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.SingleVal), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.StringVal), "a", "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly1 = new T().GetDefault1Record(new T()).TimeOnlyVal;
                var tempTimeOnly2 = new T().GetDefault2Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.TimeOnlyVal), tempTimeOnly1.ToString("o"), tempTimeOnly2.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString(), TimeSpan.FromMilliseconds(2).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.UInt128Val), "1", "2").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.UInt64Val), "1", "2").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.UInt32Val), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.UInt16Val), "1", "2").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task SyncNullIsInSetStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullBoolVal), "true").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullUInt128Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task SyncNullIsInSetRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullBoolVal), "true").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt128Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task SyncNullIsInSetRequestAllTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullBoolVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullByteVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullCharVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateTimeOffsetVal), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateOnlyVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateTimeVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDecimalVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDoubleVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullFloatVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullGuidVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullIntVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullLongVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullSByteVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullShortVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullSingleVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullStringVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullTimeOnlyVal), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullTimeSpanVal), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt128Val), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt64Val), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt32Val), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt16Val), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task SyncNullCopyIsInSetStandardTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullBoolVal), "true").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullUInt128Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsInSet(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task SyncNullCopyIsInSetRequestTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullBoolVal), "true").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt128Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsInSet(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }
    }
}