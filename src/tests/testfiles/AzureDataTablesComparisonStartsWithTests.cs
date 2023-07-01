using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit
{
    public class AzureDataTablesComparisonStartsWithTests : BaseTest
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
        public void StartsWithStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            // Single
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });
            //testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 1);

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //            });
        }

        [Fact]
        public void StartsWithRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });
            //testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 1);

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            });
        }

        [Fact]
        public void NullStartsWithStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Short
            //serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            // Single
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });
            //testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 1);

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.Apply(sourceQueryable);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //            });
        }

        [Fact]
        public void NullStartsWithRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();
            Pageable<AzureDataTablesTestClass> pagableResult = null;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            //// Short
            //serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //});

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });
            //testList = new List<AzureDataTablesTestClass>();
            //    foreach (Page<AzureDataTablesTestClass> page in pagableResult.AsPages())
            //        testList.AddRange(page.Values);
            //    Assert.NotNull(testList);
            //    Assert.True(testList.Count == 1);

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }

            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                Assert.Throws<ServiceQueryException>(() =>
            //                {
            //                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().StartsWith(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            Assert.Throws<ServiceQueryException>(() =>
            //            {
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //            });
        }
    }
}