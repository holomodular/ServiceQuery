using Azure;
using Azure.Data.Tables;

namespace ServiceQuery.Xunit
{
    public class AzureDataTablesTestClass : ITableEntity
    {
        public int Key { get; set; }
        public int SharedParentKey { get; set; }

        public string StringVal { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? NullStringVal { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        //Not supported
        //public byte ByteVal { get; set; }
        //public char CharVal { get; set; }
        public byte[] ByteArrayVal { get; set; }

        public byte?[] NullByteArrayVal { get; set; }

        public double DoubleVal { get; set; }
        public double? NullDoubleVal { get; set; }

        public bool BoolVal { get; set; }
        public bool? NullBoolVal { get; set; }

        //public DateOnly DateOnlyVal { get; set; }
        //public DateOnly? NullDateOnlyVal { get; set; }

        public DateTime DateTimeVal { get; set; }
        public DateTime? NullDateTimeVal { get; set; }

        public decimal DecimalVal { get; set; }
        public decimal? NullDecimalVal { get; set; }

        public Guid GuidVal { get; set; }
        public Guid? NullGuidVal { get; set; }

        public DateTimeOffset DateTimeOffsetVal { get; set; }
        public DateTimeOffset? NullDateTimeOffsetVal { get; set; }

        //public short ShortVal { get; set; }
        //public short? NullShortVal { get; set; }

        public int IntVal { get; set; }
        public int? NullIntVal { get; set; }

        public long LongVal { get; set; }
        public long? NullLongVal { get; set; }

        //public sbyte SByteVal { get; set; }
        //public sbyte? NullSByteVal { get; set; }

        public Single SingleVal { get; set; }
        public Single? NullSingleVal { get; set; }

        //public TimeOnly TimeOnlyVal { get; set; }
        //public TimeOnly? NullTimeOnlyVal { get; set; }

        //public TimeSpan TimeSpanVal { get; set; }
        //public UInt128 UInt128Val { get; set; }
        //public UInt16 UInt16Val { get; set; }

        //public UInt32 UInt32Val { get; set; }
        //public UInt64 UInt64Val { get; set; }

        public float FloatVal { get; set; }
        public float? NullFloatVal { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public static List<AzureDataTablesTestClass> GetDefaultList()
        {
            //added out of order
            return new List<AzureDataTablesTestClass>()
            {
                GetDefault3Record(),
                GetDefault0Record(),
                GetDefault2Record(),
                GetDefault1Record(),
            };
        }

        public static AzureDataTablesTestClass GetDefault0Record()
        {
            return new AzureDataTablesTestClass()
            {
                BoolVal = false,
                ByteArrayVal = new byte[] { 0 },
                //ByteVal = 0,
                //CharVal = ' ',
                //DateOnlyVal = new DateOnly(2000, 1, 1),
                DateTimeOffsetVal = new DateTimeOffset(2000, 1, 1, 0, 0, 0, 0, TimeSpan.Zero),
                DateTimeVal = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                DecimalVal = 0,
                DoubleVal = 0,
                FloatVal = 0,
                GuidVal = Guid.Empty,
                IntVal = 0,
                LongVal = 0,
                //ShortVal = 0,
                //SByteVal = 0,
                SingleVal = 0,
                StringVal = " ",
                //TimeOnlyVal = new TimeOnly(0, 0, 0),
                //TimeSpanVal = TimeSpan.Zero,
#if NET7_0_OR_GREATER
                //UInt128Val = 0,
#endif
                //UInt16Val = 0,
                //UInt32Val = 0,
                //UInt64Val = 0,
                PartitionKey = "",
                RowKey = "0",
                SharedParentKey = 1
            };
        }

        public static AzureDataTablesTestClass GetDefault1Record()
        {
            return new AzureDataTablesTestClass()
            {
                BoolVal = true,
                ByteArrayVal = new byte[] { 1 },
                //ByteVal = 1,
                //CharVal = 'a',
                //DateOnlyVal = new DateOnly(2001, 1, 1),
                DateTimeOffsetVal = new DateTimeOffset(2001, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                DateTimeVal = new DateTime(2001, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                DecimalVal = 1,
                DoubleVal = 1,
                FloatVal = 1,
                GuidVal = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                IntVal = 1,
                LongVal = 1,
                //ShortVal = 1,
                //SByteVal = 1,
                SingleVal = 1,
                StringVal = "a",
                //TimeOnlyVal = new TimeOnly(1, 1, 1),
                //TimeSpanVal = TimeSpan.FromMilliseconds(1),
#if NET7_0_OR_GREATER
                //UInt128Val = 1,
#endif
                //UInt16Val = 1,
                //UInt32Val = 1,
                //UInt64Val = 1,
                PartitionKey = "",
                RowKey = "1",
                SharedParentKey = 1
            };
        }

        public static AzureDataTablesTestClass GetDefault2Record()
        {
            return new AzureDataTablesTestClass()
            {
                BoolVal = false,
                ByteArrayVal = new byte[] { 2 },
                //ByteVal = 2,
                //CharVal = 'b',
                //DateOnlyVal = new DateOnly(2002, 2, 2),
                DateTimeOffsetVal = new DateTimeOffset(2002, 2, 2, 2, 2, 2, 2, TimeSpan.Zero),
                DateTimeVal = new DateTime(2002, 2, 2, 2, 2, 2, 2, DateTimeKind.Utc),
                DecimalVal = 2,
                DoubleVal = 2,
                FloatVal = 2,
                GuidVal = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                IntVal = 2,
                LongVal = 2,
                //ShortVal = 2,
                //SByteVal = 2,
                SingleVal = 2,
                StringVal = "b",
                //TimeOnlyVal = new TimeOnly(2, 2, 2),
                //TimeSpanVal = TimeSpan.FromMilliseconds(2),
#if NET7_0_OR_GREATER
                //UInt128Val = 2,
#endif
                //UInt16Val = 2,
                //UInt32Val = 2,
                //UInt64Val = 2,
                PartitionKey = "",
                RowKey = "2",
                SharedParentKey = 1
            };
        }

        public static AzureDataTablesTestClass GetDefault3Record()
        {
            return new AzureDataTablesTestClass()
            {
                BoolVal = true,
                ByteArrayVal = new byte[] { 3 },
                //ByteVal = 3,
                //CharVal = 'c',
                //DateOnlyVal = new DateOnly(2003, 3, 3),
                DateTimeOffsetVal = new DateTimeOffset(2003, 3, 3, 3, 3, 3, 3, TimeSpan.Zero),
                DateTimeVal = new DateTime(2003, 3, 3, 3, 3, 3, 3, DateTimeKind.Utc),
                DecimalVal = 3,
                DoubleVal = 3,
                FloatVal = 3,
                GuidVal = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                IntVal = 3,
                LongVal = 3,
                //ShortVal = 3,
                //SByteVal = 3,
                SingleVal = 3,
                StringVal = "c",
                //TimeOnlyVal = new TimeOnly(3, 3, 3),
                //TimeSpanVal = TimeSpan.FromMilliseconds(3),
#if NET7_0_OR_GREATER
                //UInt128Val = 3,
#endif
                //UInt16Val = 3,
                //UInt32Val = 3,
                //UInt64Val = 3,
                PartitionKey = "",
                RowKey = "3",
                SharedParentKey = 1
            };
        }
    }
}