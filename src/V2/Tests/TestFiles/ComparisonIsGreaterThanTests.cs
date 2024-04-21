namespace ServiceQuery.Xunit
{
    public class ComparisonIsGreaterThanTests : ComparisonIsGreaterThanTests<TestClass>
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
    }

    public abstract class ComparisonIsGreaterThanTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public void SyncIsGreaterThanStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.DateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.TimeOnlyVal), tempTimeOnly.ToString("o")).Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.UInt128Val), "1").Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public void IsGreaterThanRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

#if NET6_0_OR_GREATER

            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.DateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.TimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.UInt128Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public void SyncNullIsGreaterThanStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
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
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt128Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void SyncNullIsGreaterThanRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullCharVal), "b").Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt128Val), "1").Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void SyncNullCopyIsGreaterThanStandardTest()
        {
            var sourceQueryable = GetTestNullCopyList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt128Val), "1").Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public void SyncNullCopyIsGreaterThanRequestTest()
        {
            var sourceQueryable = GetTestNullCopyList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt128Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThan(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }
    }
}