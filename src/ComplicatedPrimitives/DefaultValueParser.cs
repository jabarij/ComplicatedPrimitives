using System;

namespace ComplicatedPrimitives
{
    /// <include
    ///   file='ComplicatedPrimitives.xml'
    ///   path='//member[@name="T:ComplicatedPrimitives.DefaultValueParser"]' />
    public class DefaultValueParser :
        IParser<byte>,
        IParser<short>,
        IParser<ushort>,
        IParser<int>,
        IParser<uint>,
        IParser<long>,
        IParser<ulong>,
        IParser<float>,
        IParser<double>,
        IParser<decimal>,
        IParser<DateTime>,
        IParser<DateTimeOffset>,
        IParser<TimeSpan>
    {
        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Byte}#Parse(System.String)"]' />
        byte IParser<byte>.Parse(string str) => byte.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Byte}#TryParse(System.String,System.Byte@)"]' />
        bool IParser<byte>.TryParse(string str, out byte result) => byte.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Int16}#Parse(System.String)"]' />
        short IParser<short>.Parse(string str) => short.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Int16}#TryParse(System.String,System.Int16@)"]' />
        bool IParser<short>.TryParse(string str, out short result) => short.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#UInt16}#Parse(System.String)"]' />
        ushort IParser<ushort>.Parse(string str) => ushort.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#UInt16}#TryParse(System.String,System.UInt16@)"]' />
        bool IParser<ushort>.TryParse(string str, out ushort result) => ushort.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Int32}#Parse(System.String)"]' />
        int IParser<int>.Parse(string str) => int.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Int32}#TryParse(System.String,System.Int32@)"]' />
        bool IParser<int>.TryParse(string str, out int result) => int.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#UInt32}#Parse(System.String)"]' />
        uint IParser<uint>.Parse(string str) => uint.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#UInt32}#TryParse(System.String,System.UInt32@)"]' />
        bool IParser<uint>.TryParse(string str, out uint result) => uint.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Int64}#Parse(System.String)"]' />
        long IParser<long>.Parse(string str) => long.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Int64}#TryParse(System.String,System.Int64@)"]' />
        bool IParser<long>.TryParse(string str, out long result) => long.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#UInt64}#Parse(System.String)"]' />
        ulong IParser<ulong>.Parse(string str) => ulong.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#UInt64}#TryParse(System.String,System.UInt64@)"]' />
        bool IParser<ulong>.TryParse(string str, out ulong result) => ulong.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Single}#Parse(System.String)"]' />
        float IParser<float>.Parse(string str) => float.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Single}#TryParse(System.String,System.Single@)"]' />
        bool IParser<float>.TryParse(string str, out float result) => float.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Double}#Parse(System.String)"]' />
        double IParser<double>.Parse(string str) => double.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Double}#TryParse(System.String,System.Double@)"]' />
        bool IParser<double>.TryParse(string str, out double result) => double.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Decimal}#Parse(System.String)"]' />
        decimal IParser<decimal>.Parse(string str) => decimal.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#Decimal}#TryParse(System.String,System.Decimal@)"]' />
        bool IParser<decimal>.TryParse(string str, out decimal result) => decimal.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#DateTime}#Parse(System.String)"]' />
        DateTime IParser<DateTime>.Parse(string str) => DateTime.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#DateTime}#TryParse(System.String,System.DateTime@)"]' />
        bool IParser<DateTime>.TryParse(string str, out DateTime result) => DateTime.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#DateTimeOffset}#Parse(System.String)"]' />
        DateTimeOffset IParser<DateTimeOffset>.Parse(string str) => DateTimeOffset.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#DateTimeOffset}#TryParse(System.String,System.DateTimeOffset@)"]' />
        bool IParser<DateTimeOffset>.TryParse(string str, out DateTimeOffset result) => DateTimeOffset.TryParse(str, out result);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#TimeSpan}#Parse(System.String)"]' />
        TimeSpan IParser<TimeSpan>.Parse(string str) => TimeSpan.Parse(str);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.DefaultValueParser.ComplicatedPrimitives#IParser{System#TimeSpan}#TryParse(System.String,System.TimeSpan@)"]' />
        bool IParser<TimeSpan>.TryParse(string str, out TimeSpan result) => TimeSpan.TryParse(str, out result);
    }
}
