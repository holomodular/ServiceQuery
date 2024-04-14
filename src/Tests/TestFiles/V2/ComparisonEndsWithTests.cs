namespace ServiceQuery.Xunit
{
    public class ComparisonEndsWithTests : ComparisonEndsWithTests<TestClass>
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

    public abstract class ComparisonEndsWithTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public void SyncEndsWithStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.ByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.CharVal), "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.DateOnlyVal), tempDateOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif
            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.DecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.DoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.FloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.IntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.LongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.SByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.ShortVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.SingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.StringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.TimeOnlyVal), tempTimeOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.UInt128Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.UInt64Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.UInt32Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.UInt16Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
        }

        [Fact]
        public void SyncEndsWithRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.ByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.CharVal), "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.DateOnlyVal), tempDateOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.DecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.DoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.FloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.IntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.LongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.SByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.ShortVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.SingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.StringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.TimeOnlyVal), tempTimeOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.UInt128Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.UInt64Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.UInt32Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.UInt16Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
        }

        [Fact]
        public void SyncNullEndsWithStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullCharVal), "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullDecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullDoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullFloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullIntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullLongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullSByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullShortVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullSingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullStringVal), "a").Build();
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
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullUInt128Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullUInt64Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullUInt32Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullUInt16Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
        }

        [Fact]
        public void SyncNullEndsWithRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullCharVal), "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullDecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullDoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullFloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullIntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullLongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullSByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullShortVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullSingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullStringVal), "a").Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullUInt128Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullUInt64Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullUInt32Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullUInt16Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
        }

        [Fact]
        public void SyncNullCopyEndsWithStandardTest()
        {
            var sourceQueryable = GetTestNullCopyList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullCharVal), "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullDecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullDoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullFloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullIntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullLongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullSByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullShortVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullSingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullUInt128Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullUInt64Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullUInt32Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(TestClass.NullUInt16Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
        }

        [Fact]
        public void SyncNullCopyEndsWithRequestTest()
        {
            var sourceQueryable = GetTestNullCopyList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullCharVal), "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullDateOnlyVal), tempDateOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullDecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullDoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullFloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullIntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullLongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullSByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullShortVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullSingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullTimeOnlyVal), tempTimeOnly.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullUInt128Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullUInt64Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullUInt32Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.NullUInt16Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
        }
    }
}