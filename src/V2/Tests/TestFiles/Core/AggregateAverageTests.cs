﻿namespace ServiceQuery.Xunit
{
    public class AggregateAverageTests : AggregateAverageTests<TestClass>
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

    public abstract class AggregateAverageTests<T> : BaseTest<T> where T : class
    {
        public bool ValidateNullLong = true;

        public bool CosmosIntLongRounding = false;

        [Fact]
        public void AverageStandardTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.BoolVal)).Build();
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
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.ByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.ByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.CharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DecimalVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.FloatVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            if (CosmosIntLongRounding)
            {
                // Int
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.IntVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == Math.Round((double)6 / 4));

                // Long
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.LongVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == Math.Round((double)6 / 4));
            }
            else
            {
                // Int
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.IntVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);

                // Long
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.LongVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);
            }

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.SByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.ShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.SingleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // String
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.StringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.TimeOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.TimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.UInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.UInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.UInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.UInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
        }

        [Fact]
        public void AverageRequestTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.BoolVal)).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.ByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.ByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.CharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER

            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DecimalVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.FloatVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            if (CosmosIntLongRounding)
            {
                // Int
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.IntVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == Math.Round((double)6 / 4));

                // Long
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.LongVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == Math.Round((double)6 / 4));
            }
            else
            {
                // Int
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.IntVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);

                // Long
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.LongVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);
            }

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.SByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.ShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.SingleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.StringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER

            // TimeOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.TimeOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.TimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER

            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.UInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.UInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.UInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.UInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
        }

        [Fact]
        public void NullAverageStandardTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullBoolVal)).Build();
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
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullCharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDecimalVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullFloatVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullGuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Long
            if (ValidateNullLong)
            {
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullSByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullSingleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // String
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullStringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullTimeOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullTimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullUInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullUInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullUInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullUInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
        }

        [Fact]
        public void NullAverageRequestTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullBoolVal)).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullCharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDecimalVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullFloatVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullGuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Long
            if (ValidateNullLong)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullSByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullSingleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullStringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullTimeOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullTimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullUInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullUInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullUInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullUInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
        }
    }
}