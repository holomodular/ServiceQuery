namespace ServiceQuery.Xunit
{
    public class ComparisonIsNotNullTests : ComparisonIsNotNullTests<TestClass>
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

    public abstract class ComparisonIsNotNullTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public void SyncIsNotNullStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.BoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.ByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.CharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.DateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.DecimalVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.DoubleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.FloatVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.IntVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.LongVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.SByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.ShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.SingleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.StringVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // String?
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.TimeOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.TimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            // UInt128
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.UInt128Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.UInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.UInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.UInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
        }

        [Fact]
        public void SyncIsNotNullRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.BoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.ByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.CharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.DateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.DecimalVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.DoubleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.FloatVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.IntVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.LongVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.SByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.ShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.SingleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.StringVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.TimeOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.TimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.UInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.UInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.UInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.UInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
        }

        [Fact]
        public void SyncNullIsNotNullStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullCharVal)).Build();
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
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullDateTimeOffsetVal)).Build();
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
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullDecimalVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullTimeOnlyVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullTimeSpanVal)).Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullUInt128Val)).Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void SyncNullIsNotNullRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullCharVal)).Build();
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
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDateTimeOffsetVal)).Build();
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
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDecimalVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullTimeOnlyVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullTimeSpanVal)).Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt128Val)).Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void SyncFoundNullIsNotNullRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullCharVal)).Build();
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
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDateTimeOffsetVal)).Build();
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
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDecimalVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER

            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullTimeOnlyVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullTimeSpanVal)).Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt128Val)).Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void SyncNullCopyIsNotNullStandardTest()
        {
            var sourceQueryable = GetTestNullCopyList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullCharVal)).Build();
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
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullDateTimeOffsetVal)).Build();
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
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullDecimalVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullTimeOnlyVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullTimeSpanVal)).Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullUInt128Val)).Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNotNull(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public void SyncNullCopyIsNotNullRequestTest()
        {
            var sourceQueryable = GetTestNullCopyList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullCharVal)).Build();
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
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDateTimeOffsetVal)).Build();
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
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDecimalVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullTimeOnlyVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullTimeSpanVal)).Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt128Val)).Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNotNull(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }
    }
}