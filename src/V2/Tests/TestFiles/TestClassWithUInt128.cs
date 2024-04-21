using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceQuery.Xunit
{
    public class TestClassWithUInt128 : TestClass
    {
#if NET7_0
        public UInt128 UInt128Val { get; set; }
#endif

        public static List<TestClassWithUInt128> GetDefaultList()
        {
            //added out of order
            return new List<TestClassWithUInt128>()
            {
                GetDefault3Record(),
                GetDefault0Record(),
                GetDefault2Record(),
                GetDefault1Record(),
            };
        }

        public static TestClassWithUInt128 GetDefault0Record()
        {
            return new TestClassWithUInt128()
            {
                BoolVal = false,
                ByteArrayVal = new byte[] { 0 },
                ByteVal = 0,
                CharVal = ' ',
                DateTimeOffsetVal = new DateTimeOffset(2000, 1, 1, 0, 0, 0, 0, TimeSpan.Zero),
                DateTimeVal = new DateTime(2000, 1, 1, 0, 0, 0, 0),
                DecimalVal = 0,
                DoubleVal = 0,
                FloatVal = 0,
                GuidVal = Guid.Empty,
                IntVal = 0,
                LongVal = 0,
                ShortVal = 0,
                SByteVal = 0,
                SingleVal = 0,
                StringVal = " ",
                TimeSpanVal = TimeSpan.Zero,
#if NET7_0
                UInt128Val = 0,
#endif
                UInt16Val = 0,
                UInt32Val = 0,
                UInt64Val = 0
            };
        }

        public static TestClassWithUInt128 GetDefault1Record()
        {
            return new TestClassWithUInt128()
            {
                BoolVal = true,
                ByteArrayVal = new byte[] { 1 },
                ByteVal = 1,
                CharVal = 'a',
                DateTimeOffsetVal = new DateTimeOffset(2001, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                DateTimeVal = new DateTime(2001, 1, 1, 1, 1, 1, 1),
                DecimalVal = 1,
                DoubleVal = 1,
                FloatVal = 1,
                GuidVal = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                IntVal = 1,
                LongVal = 1,
                ShortVal = 1,
                SByteVal = 1,
                SingleVal = 1,
                StringVal = "a",
                TimeSpanVal = TimeSpan.FromMilliseconds(1),
#if NET7_0
                UInt128Val = 1,
#endif
                UInt16Val = 1,
                UInt32Val = 1,
                UInt64Val = 1
            };
        }

        public static TestClassWithUInt128 GetDefault2Record()
        {
            return new TestClassWithUInt128()
            {
                BoolVal = false,
                ByteArrayVal = new byte[] { 2 },
                ByteVal = 2,
                CharVal = 'b',
                DateTimeOffsetVal = new DateTimeOffset(2002, 2, 2, 2, 2, 2, 2, TimeSpan.Zero),
                DateTimeVal = new DateTime(2002, 2, 2, 2, 2, 2, 2),
                DecimalVal = 2,
                DoubleVal = 2,
                FloatVal = 2,
                GuidVal = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                IntVal = 2,
                LongVal = 2,
                ShortVal = 2,
                SByteVal = 2,
                SingleVal = 2,
                StringVal = "b",
                TimeSpanVal = TimeSpan.FromMilliseconds(2),
#if NET7_0
                UInt128Val = 2,
#endif
                UInt16Val = 2,
                UInt32Val = 2,
                UInt64Val = 2
            };
        }

        public static TestClassWithUInt128 GetDefault3Record()
        {
            return new TestClassWithUInt128()
            {
                BoolVal = true,
                ByteArrayVal = new byte[] { 3 },
                ByteVal = 3,
                CharVal = 'c',
                DateTimeOffsetVal = new DateTimeOffset(2003, 3, 3, 3, 3, 3, 3, TimeSpan.Zero),
                DateTimeVal = new DateTime(2003, 3, 3, 3, 3, 3, 3),
                DecimalVal = 3,
                DoubleVal = 3,
                FloatVal = 3,
                GuidVal = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                IntVal = 3,
                LongVal = 3,
                ShortVal = 3,
                SByteVal = 3,
                SingleVal = 3,
                StringVal = "c",
                TimeSpanVal = TimeSpan.FromMilliseconds(3),
#if NET7_0
                UInt128Val = 3,
#endif
                UInt16Val = 3,
                UInt32Val = 3,
                UInt64Val = 3
            };
        }
    }
}