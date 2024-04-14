namespace ServiceQuery.Xunit
{
    public class AggregateSumTests : AggregateSumTests<TestClass>
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

    public abstract class AggregateSumTests<T> : BaseTest<T> where T : class
    {
        public bool NullSumIsNull = false;

        [Fact]
        public void SumStandardTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.BoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.ByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.ByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.CharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DecimalVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == 6);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DoubleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.FloatVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.IntVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.LongVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.SByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.ShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.SingleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.StringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.TimeOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.TimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.UInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.UInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.UInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.UInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
        }

        [Fact]
        public void SumRequestTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.BoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.ByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.ByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.CharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DecimalVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == 6);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DoubleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.FloatVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.IntVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.LongVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.SByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.ShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.SingleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 6);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.StringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.TimeOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.TimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.UInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.UInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.UInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.UInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
        }

        [Fact]
        public void NullSumStandardTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullBoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullCharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDecimalVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDoubleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullFloatVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullGuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullIntVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullLongVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullSByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullSingleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullStringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullTimeOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullTimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullUInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullUInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullUInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullUInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
        }

        [Fact]
        public void NullSumRequestTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullBoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullCharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDecimalVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDoubleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullFloatVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullGuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullIntVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullLongVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullSByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullSingleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            if (NullSumIsNull)
                Assert.True(result == null);
            else
                Assert.True(result == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullStringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullTimeOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullTimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullUInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullUInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullUInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullUInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
        }
    }
}