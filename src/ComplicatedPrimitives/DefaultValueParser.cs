using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Class wrapping default .NET parsing functions to provide them as <see cref="IParser{T}"/> implementations.
    /// </summary>
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
        /// <summary>
        /// Converts the specified string representation of a number to its <see cref="byte"/> equivalent using <see cref="byte.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <returns>
        /// A byte value that is equivalent to the number contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="byte.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.byte.parse">docs</seealso>.
        /// </remarks>
        byte IParser<byte>.Parse(string str) => byte.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="byte"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="byte"/> value equivalent to the number contained in <paramref name="str"/> if the conversion succeeded,
        /// or zero if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="byte.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.byte.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<byte>.TryParse(string str, out byte result) => byte.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a number to its <see cref="short"/> equivalent using <see cref="short.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <returns>
        /// A byte value that is equivalent to the number contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="short.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.int16.parse">docs</seealso>.
        /// </remarks>
        short IParser<short>.Parse(string str) => short.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="short"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="short"/> value equivalent to the number contained in <paramref name="str"/> if the conversion succeeded,
        /// or zero if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="short.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.int16.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<short>.TryParse(string str, out short result) => short.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a number to its <see cref="ushort"/> equivalent using <see cref="ushort.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <returns>
        /// A byte value that is equivalent to the number contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="ushort.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.uint16.parse">docs</seealso>.
        /// </remarks>
        ushort IParser<ushort>.Parse(string str) => ushort.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="ushort"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="ushort"/> value equivalent to the number contained in <paramref name="str"/> if the conversion succeeded,
        /// or zero if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="ushort.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.uint16.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<ushort>.TryParse(string str, out ushort result) => ushort.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a number to its <see cref="int"/> equivalent using <see cref="int.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <returns>
        /// A byte value that is equivalent to the number contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="int.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.int32.parse">docs</seealso>.
        /// </remarks>
        int IParser<int>.Parse(string str) => int.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="int"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="int"/> value equivalent to the number contained in <paramref name="str"/> if the conversion succeeded,
        /// or zero if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="int.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<int>.TryParse(string str, out int result) => int.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a number to its <see cref="uint"/> equivalent using <see cref="uint.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <returns>
        /// A byte value that is equivalent to the number contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="uint.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.uint32.parse">docs</seealso>.
        /// </remarks>
        uint IParser<uint>.Parse(string str) => uint.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="uint"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="uint"/> value equivalent to the number contained in <paramref name="str"/> if the conversion succeeded,
        /// or zero if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="uint.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.uint32.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<uint>.TryParse(string str, out uint result) => uint.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a number to its <see cref="long"/> equivalent using <see cref="long.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <returns>
        /// A byte value that is equivalent to the number contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="long.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.int64.parse">docs</seealso>.
        /// </remarks>
        long IParser<long>.Parse(string str) => long.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="long"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="long"/> value equivalent to the number contained in <paramref name="str"/> if the conversion succeeded,
        /// or zero if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="long.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.int64.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<long>.TryParse(string str, out long result) => long.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a number to its <see cref="ulong"/> equivalent using <see cref="ulong.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <returns>
        /// A byte value that is equivalent to the number contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="ulong.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.uint64.parse">docs</seealso>.
        /// </remarks>
        ulong IParser<ulong>.Parse(string str) => ulong.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="ulong"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="ulong"/> value equivalent to the number contained in <paramref name="str"/> if the conversion succeeded,
        /// or zero if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="ulong.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.uint64.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<ulong>.TryParse(string str, out ulong result) => ulong.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a number to its <see cref="float"/> equivalent using <see cref="float.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <returns>
        /// A byte value that is equivalent to the number contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="float.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.single.parse">docs</seealso>.
        /// </remarks>
        float IParser<float>.Parse(string str) => float.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="float"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="float"/> value equivalent to the number contained in <paramref name="str"/> if the conversion succeeded,
        /// or zero if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="float.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.single.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<float>.TryParse(string str, out float result) => float.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a number to its <see cref="double"/> equivalent using <see cref="double.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <returns>
        /// A byte value that is equivalent to the number contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="double.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.double.parse">docs</seealso>.
        /// </remarks>
        double IParser<double>.Parse(string str) => double.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="double"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="double"/> value equivalent to the number contained in <paramref name="str"/> if the conversion succeeded,
        /// or zero if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="double.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.double.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<double>.TryParse(string str, out double result) => double.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a number to its <see cref="decimal"/> equivalent using <see cref="decimal.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <returns>
        /// A byte value that is equivalent to the number contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="decimal.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.decimal.parse">docs</seealso>.
        /// </remarks>
        decimal IParser<decimal>.Parse(string str) => decimal.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="decimal"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the <see cref="decimal"/> value equivalent to the number contained in <paramref name="str"/> if the conversion succeeded,
        /// or zero if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="decimal.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.decimal.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<decimal>.TryParse(string str, out decimal result) => decimal.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using <see cref="DateTime.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a date and time to convert.</param>
        /// <returns>
        /// An object that is equivalent to the date and time that is contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="DateTime.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset.parse">docs</seealso>.
        /// </remarks>
        DateTime IParser<DateTime>.Parse(string str) => DateTime.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a date and time to its <see cref="DateTime"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a date and time to convert.</param>
        /// <param name="result">
        /// When the method returns, contains the <see cref="DateTime"/> equivalent to the date and time of <paramref name="str"/>, if the conversion succeeded, or <see cref="DateTime.MinValue"/>, if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="DateTime.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<DateTime>.TryParse(string str, out DateTime result) => DateTime.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTimeOffset"/> equivalent using <see cref="DateTimeOffset.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a date and time to convert.</param>
        /// <returns>
        /// An object that is equivalent to the date and time that is contained in <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="DateTimeOffset.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset.parse">docs</seealso>.
        /// </remarks>
        DateTimeOffset IParser<DateTimeOffset>.Parse(string str) => DateTimeOffset.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a date and time to its <see cref="DateTimeOffset"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a date and time to convert.</param>
        /// <param name="result">
        /// When the method returns, contains the <see cref="DateTimeOffset"/> equivalent to the date and time of <paramref name="str"/>, if the conversion succeeded, or <see cref="DateTimeOffset.MinValue"/>, if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="DateTimeOffset.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<DateTimeOffset>.TryParse(string str, out DateTimeOffset result) => DateTimeOffset.TryParse(str, out result);

        /// <summary>
        /// Converts the specified string representation of a time interval to its <see cref="TimeSpan"/> equivalent using <see cref="TimeSpan.Parse(string)"/> method.
        /// </summary>
        /// <param name="str">A string that contains a time interval to convert.</param>
        /// <returns>
        /// A time interval that corresponds to <paramref name="str"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="TimeSpan.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.timespan.parse">docs</seealso>.
        /// </remarks>
        TimeSpan IParser<TimeSpan>.Parse(string str) => TimeSpan.Parse(str);

        /// <summary>
        /// Tries to convert the string representation of a time interval to its <see cref="TimeSpan"/> equivalent, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains a time interval to convert.</param>
        /// <param name="result">
        /// When this method returns, contains an object that represents the time interval specified by <paramref name="str"/>, or <see cref="TimeSpan.Zero"/> if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// For more defails, see underlying method <seealso cref="TimeSpan.Parse(string)" href="https://docs.microsoft.com/en-us/dotnet/api/system.timespan.tryparse">docs</seealso>.
        /// </remarks>
        bool IParser<TimeSpan>.TryParse(string str, out TimeSpan result) => TimeSpan.TryParse(str, out result);
    }
}
