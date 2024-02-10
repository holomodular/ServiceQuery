namespace ServiceQuery.Xunit
{
    public interface ITestClass
    {
        bool BoolVal { get; set; }
        byte[] ByteArrayVal { get; set; }
        byte ByteVal { get; set; }
        char CharVal { get; set; }
        DateTimeOffset DateTimeOffsetVal { get; set; }
        DateTime DateTimeVal { get; set; }
        decimal DecimalVal { get; set; }
        double DoubleVal { get; set; }
        float FloatVal { get; set; }
        Guid GuidVal { get; set; }
        int IntVal { get; set; }
        long LongVal { get; set; }
        bool? NullBoolVal { get; set; }
        byte?[] NullByteArrayVal { get; set; }
        byte? NullByteVal { get; set; }
        char? NullCharVal { get; set; }
        DateTimeOffset? NullDateTimeOffsetVal { get; set; }
        DateTime? NullDateTimeVal { get; set; }
        decimal? NullDecimalVal { get; set; }
        double? NullDoubleVal { get; set; }
        float? NullFloatVal { get; set; }
        Guid? NullGuidVal { get; set; }
        int? NullIntVal { get; set; }
        long? NullLongVal { get; set; }
        sbyte? NullSByteVal { get; set; }
        short? NullShortVal { get; set; }
        float? NullSingleVal { get; set; }
        string NullStringVal { get; set; }
        TimeSpan? NullTimeSpanVal { get; set; }
        ushort? NullUInt16Val { get; set; }
        uint? NullUInt32Val { get; set; }
        ulong? NullUInt64Val { get; set; }
        sbyte SByteVal { get; set; }
        int SharedParentKey { get; set; }
        short ShortVal { get; set; }
        float SingleVal { get; set; }
        string StringVal { get; set; }
        TimeSpan TimeSpanVal { get; set; }
        ushort UInt16Val { get; set; }
        uint UInt32Val { get; set; }
        ulong UInt64Val { get; set; }

        void CopyToNullVals();

        ITestClass GetDefault0Record();

        ITestClass GetDefault1Record();

        ITestClass GetDefault2Record();

        ITestClass GetDefault3Record();

        List<TestClass> GetDefaultList();
    }
}