using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit.Integration
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesComparisonEndsWithTests : BaseTest
    {
        private TableClient _tableClient = null;

        protected void Startup()
        {
            _tableClient = new TableClient(AzureDataTablesHelper.GetConnectionString(), "ServiceQueryTestClasses");
            _tableClient.CreateIfNotExists();
            List<AzureDataTablesTestClass> testClasses = new List<AzureDataTablesTestClass>();
            var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(maxPerPage: 1000);
            foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
                testClasses.AddRange(page.Values);
            if (testClasses.Count != 4)
            {
                var testlist = AzureDataTablesTestClass.GetDefaultList();
                foreach (var item in testlist)
                    _tableClient.AddEntity(item);
            }
        }

        [Fact]
        public void EndsWithStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // SByte
            //serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            // Single
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });
            //var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 1);

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    Assert.Throws<ServiceQueryException>(() =>
            //    {
            //        testQueryable = serviceQuery.Apply(sourceQueryable);
            //    });
            //}

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //            });
        }

        [Fact]
        public void EndsWithRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.ByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.CharVal), "b").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.DecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.DoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.FloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.IntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.LongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.SByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.ShortVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.SingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.StringVal), "a").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });
            //var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 1);

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    Assert.Throws<ServiceQueryException>(() =>
            //    {
            //        testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    });
            //}

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.UInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.UInt64Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.UInt32Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(TestClass.UInt16Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            });
        }

        [Fact]
        public void NullEndsWithStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // SByte
            //serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            // Single
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });
            //var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 1);

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    Assert.Throws<ServiceQueryException>(() =>
            //    {
            //        testQueryable = serviceQuery.Apply(sourceQueryable);
            //    });
            //}

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //            });
        }

        [Fact]
        public void NullEndsWithRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });
            //var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //testList = new List<AzureDataTablesTestClass>();
            //foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //    testList.AddRange(page.Values);
            //Assert.NotNull(testList);
            //Assert.True(testList.Count == 1);

            //if (ValidateTimeSpan)
            //{
            //    // TimeSpan
            //    serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //    Assert.Throws<ServiceQueryException>(() =>
            //    {
            //        testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //    });
            //}

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().EndsWith(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            });
        }
    }
}