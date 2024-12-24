using Azure;
using Azure.Data.Tables;
using System.Linq.Expressions;

namespace ServiceQuery.Xunit.Integration
{
    [Collection("AzureDataTables")]
    public class AzureDataTablesComparisonContainsTests : BaseTest
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
        public void ContainsStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Byte
            //serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            // Short
            //serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            // Single
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //            Assert.Throws<NotSupportedException>(() =>
            //            {
            //                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //            Assert.Throws<NotSupportedException>(() =>
            //            {
            //                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //            });
        }

        [Fact]
        public void ContainsRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.BoolVal), "true").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.ByteVal), "1").Build();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.CharVal), "b").Build();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.DecimalVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.DoubleVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.FloatVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.GuidVal), Guid.Empty.ToString()).Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.IntVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.LongVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.SByteVal), "1").Build();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            // Short
            //serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.ShortVal), "1").Build();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.SingleVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.StringVal), "a").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.UInt128Val), "1").Build();
            //                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.UInt64Val), "1").Build();
            //                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.UInt32Val), "1").Build();
            //            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //            Assert.Throws<NotSupportedException>(() =>
            //            {
            //                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.UInt16Val), "1").Build();
            //            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //            Assert.Throws<NotSupportedException>(() =>
            //            {
            //                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //            });
        }

        [Fact]
        public void NullContainsStandardTest()
        {
            Startup();
            IServiceQuery serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Byte
            //serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            //// Char
            //serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// SByte
            //serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            // Short
            //serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            // Single
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //            Assert.Throws<NotSupportedException>(() =>
            //            {
            //                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            selectProperties = serviceQuery.GetSelectProperties<AzureDataTablesTestClass>();
            //            predicate = serviceQuery.BuildWhereExpression<AzureDataTablesTestClass>();
            //            Assert.Throws<NotSupportedException>(() =>
            //            {
            //                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //            });
        }

        [Fact]
        public void NullContainsRequestTest()
        {
            Startup();
            IServiceQueryRequest serviceQuery;
            List<string> selectProperties;
            Expression<Func<AzureDataTablesTestClass, bool>> predicate;
            List<AzureDataTablesTestClass> testList = new List<AzureDataTablesTestClass>();

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullBoolVal), "true").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Byte
            //serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullByteVal), "1").Build();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            //// Char
            //serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullCharVal), "b").Build();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new TestClass().GetDefault1Record(new TestClass()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
                });
            }

            // DateTime
            var tempDateTime = new TestClass().GetDefault1Record(new TestClass()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullDecimalVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullDoubleVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullFloatVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullIntVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullLongVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            //// SByte
            //serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullSByteVal), "1").Build();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            // Short
            //serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullShortVal), "1").Build();
            //selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //});

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullSingleVal), "1").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            Assert.Throws<ServiceQueryException>(() =>
            {
                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullStringVal), "a").Build();
            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            Assert.Throws<NotSupportedException>(() =>
            {
                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            });

            //            if (ValidateTimeSpan)
            //            {
            //                // TimeSpan
            //                serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
            //                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullUInt128Val), "1").Build();
            //                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }
            //#endif
            //            if (ValidateUInt64)
            //            {
            //                // UInt64
            //                serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullUInt64Val), "1").Build();
            //                selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //                predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //                Assert.Throws<NotSupportedException>(() =>
            //                {
            //                    var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //                });
            //            }

            //            // UInt32
            //            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullUInt32Val), "1").Build();
            //            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //            Assert.Throws<NotSupportedException>(() =>
            //            {
            //                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //            });

            //            // UInt16
            //            serviceQuery = ServiceQueryRequestBuilder.New().Contains(nameof(AzureDataTablesTestClass.NullUInt16Val), "1").Build();
            //            selectProperties = serviceQuery.GetServiceQuery().GetSelectProperties<AzureDataTablesTestClass>();
            //            predicate = serviceQuery.GetServiceQuery().BuildWhereExpression<AzureDataTablesTestClass>();
            //            Assert.Throws<NotSupportedException>(() =>
            //            {
            //                var pagableResult = _tableClient.Query<AzureDataTablesTestClass>(predicate, 1000, selectProperties);
            //            });
        }
    }
}