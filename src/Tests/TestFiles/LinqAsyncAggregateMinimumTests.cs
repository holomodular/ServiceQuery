namespace ServiceQuery.Xunit
{
    public class LinqAsyncAggregateMinimumTests : LinqAsyncAggregateMinimumTests<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }
    }

    public abstract class LinqAsyncAggregateMinimumTests<T> : BaseTest<T> where T : class
    {
        [Fact]
        public void MinimumStandardTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.BoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.ByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.ByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.CharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.DecimalVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.DoubleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);
            //result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.FloatVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.IntVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.LongVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.SByteVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.ShortVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.SingleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.StringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.TimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.UInt64Val)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.UInt32Val)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.UInt16Val)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);
        }

        [Fact]
        public void MinimumRequestTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.BoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            //await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            //{
            //    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            //});

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.ByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.ByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.CharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.DecimalVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.DoubleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);
            //result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.FloatVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.IntVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.LongVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.SByteVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.ShortVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.SingleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.StringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.TimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.UInt64Val)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.UInt32Val)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.UInt16Val)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == 0);
        }

        [Fact]
        public void NullMinimumStandardTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullBoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });
            //await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            //{
            //    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            //});

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullCharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDecimalVal)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDoubleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);
            //result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullFloatVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullGuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullIntVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullLongVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullSByteVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullShortVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullSingleVal)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // String
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullStringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullTimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                });
            }
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt64Val)).Build();
                result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt32Val)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt16Val)).Build();
            result = serviceQuery.ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);
        }

        [Fact]
        public void NullMinimumRequestTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullBoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });
            //await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            //{
            //    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            //});

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullByteArrayVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullCharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDecimalVal)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDoubleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);
            //result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullFloatVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullGuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullIntVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullLongVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullSByteVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullShortVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullSingleVal)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullStringVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullTimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                });
            }
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt64Val)).Build();
                result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt32Val)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt16Val)).Build();
            result = serviceQuery.GetServiceQuery().ExecuteAggregate<T>(sourceQueryable);
            Assert.True(result == null);
        }
    }
}