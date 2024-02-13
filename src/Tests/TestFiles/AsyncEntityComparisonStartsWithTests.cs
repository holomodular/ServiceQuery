using System.Data.Entity;

namespace ServiceQuery.Xunit
{
    public class ComparisonStartsWithTests : ComparisonStartsWithTests<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }
    }

    public abstract class ComparisonStartsWithTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public async Task StartsWithStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.ByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.CharVal), "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.DecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.DoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.FloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.IntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.LongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.SByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.ShortVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.SingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.StringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.UInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.UInt64Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.UInt32Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.UInt16Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
        }

        [Fact]
        public async Task StartsWithRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.ByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.CharVal), "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.DecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.DoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.FloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.IntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.LongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.SByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.ShortVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.SingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.StringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.UInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.UInt64Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.UInt32Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.UInt16Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
        }

        [Fact]
        public async Task NullStartsWithStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullCharVal), "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullDecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullDoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullFloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullIntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullLongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullSByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullShortVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullSingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullUInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullUInt64Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullUInt32Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(TestClass.NullUInt16Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
        }

        [Fact]
        public async Task NullStartsWithRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullCharVal), "b").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullDecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullDoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullFloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullIntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullLongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullSByteVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullShortVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullSingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullUInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullUInt64Val), "1").Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullUInt32Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(TestClass.NullUInt16Val), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
        }
    }
}