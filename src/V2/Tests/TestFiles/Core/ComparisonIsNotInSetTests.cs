namespace ServiceQuery.Xunit
{
    public class ComparisonIsNotInSetTests : ComparisonIsNotInSetTests<TestClass>
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
            return Task.FromResult(GetTestList());
        }

        public override Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return Task.FromResult(GetTestNullCopyList());
        }
    }

    public abstract class ComparisonIsNotInSetTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public void SyncIsNotInSetStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.BoolVal), "true").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.StringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.TimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt128Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
        }

        [Fact]
        public void SyncIsNotInSetTwo()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.BoolVal), "true", "false").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ByteVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.CharVal), "a", "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                var tempDateTimeOffset2 = new T().GetDefault2Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly1 = new T().GetDefault1Record(new T()).DateOnlyVal;
            var tempDateOnly2 = new T().GetDefault2Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateOnlyVal), tempDateOnly1.ToString("o"), tempDateOnly2.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#endif

            // DateTime
            var tempDateTime1 = new T().GetDefault1Record(new T()).DateTimeVal;
            var tempDateTime2 = new T().GetDefault2Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DecimalVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DoubleVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.FloatVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Guid
            var tempGuid = new T().GetDefault1Record(new T()).GuidVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempGuid.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.IntVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.LongVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SByteVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ShortVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SingleVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.StringVal), "a", "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly1 = new T().GetDefault1Record(new T()).TimeOnlyVal;
                var tempTimeOnly2 = new T().GetDefault2Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.TimeOnlyVal), tempTimeOnly1.ToString("o"), tempTimeOnly2.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString(), TimeSpan.FromMilliseconds(2).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt128Val), "1", "2").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt64Val), "1", "2").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt32Val), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt16Val), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public void SyncIsNotInSetRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.BoolVal), "true").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.DateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.StringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.TimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.UInt128Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
        }

        [Fact]
        public void SyncIsNotInSetTwoRequestTests()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.BoolVal), "true", "false").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ByteVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.CharVal), "a", "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset1 = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                var tempDateTimeOffset2 = new T().GetDefault2Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset1.ToString("o"), tempDateTimeOffset2.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly1 = new T().GetDefault1Record(new T()).DateOnlyVal;
            var tempDateOnly2 = new T().GetDefault2Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateOnlyVal), tempDateOnly1.ToString("o"), tempDateOnly2.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#endif

            // DateTime
            var tempDateTime1 = new T().GetDefault1Record(new T()).DateTimeVal;
            var tempDateTime2 = new T().GetDefault2Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DateTimeVal), tempDateTime1.ToString("o"), tempDateTime2.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DecimalVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.DoubleVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.FloatVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Guid
            var tempGuid = new T().GetDefault1Record(new T()).GuidVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.GuidVal), Guid.Empty.ToString(), tempGuid.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.IntVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.LongVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SByteVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.ShortVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.SingleVal), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.StringVal), "a", "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly1 = new T().GetDefault1Record(new T()).TimeOnlyVal;
                var tempTimeOnly2 = new T().GetDefault2Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.TimeOnlyVal), tempTimeOnly1.ToString("o"), tempTimeOnly2.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString(), TimeSpan.FromMilliseconds(2).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt128Val), "1", "2").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt64Val), "1", "2").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt32Val), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.UInt16Val), "1", "2").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public void SyncNullIsNotInSetStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullBoolVal), "true").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt128Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public void SyncNullIsNotInSetRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullBoolVal), "true").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt128Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public void SyncNullIsNotInSetRequestNoneTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullBoolVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullByteVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullCharVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeOffsetVal), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateOnlyVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDecimalVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDoubleVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullFloatVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullGuidVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullIntVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullLongVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullSByteVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullShortVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullSingleVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullStringVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullTimeOnlyVal), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullTimeSpanVal), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt128Val), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt64Val), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt32Val), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt16Val), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void SyncNullCopyIsNotInSetStandardTest()
        {
            var sourceQueryable = GetTestNullCopyList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullBoolVal), "true").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt128Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNotInSet(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
        }

        [Fact]
        public void SyncNullCopyIsNotInSetRequestTest()
        {
            var sourceQueryable = GetTestNullCopyList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullBoolVal), "true").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 3);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt128Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotInSet(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
        }
    }
}