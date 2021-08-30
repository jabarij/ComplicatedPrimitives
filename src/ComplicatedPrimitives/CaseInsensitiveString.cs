using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// Wraps System.String implicitly ignoring character case whenever it matters in case of normal string.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{_value}")]
    public struct CaseInsensitiveString : IEquatable<CaseInsensitiveString>, IEquatable<string>
    {
        /// <summary>
        /// Represents the empty case insensitive string.
        /// </summary>
        public static readonly CaseInsensitiveString Empty = new CaseInsensitiveString(string.Empty);

        private readonly string _value;

        /// <summary>
        /// Creates a new instance of <see cref="CaseInsensitiveString"/> wrapping a specified <paramref name="str"/>.
        /// </summary>
        /// <param name="str">String value to wrap as case insensitive string.</param>
        public CaseInsensitiveString(string str) => _value = str;

        /// <summary>
        /// Returns underlying string value of this instance; no actual conversion is performed.
        /// </summary>
        /// <returns>Exact original string value specified when creating this instance of <see cref="CaseInsensitiveString"/>.</returns>
        public override string ToString() => _value;

        #region Equality

        /// <summary>
        /// Determines whether this instance of <see cref="CaseInsensitiveString"/> and the <paramref name="other"/> one have the same value ignoring case differences between characters.
        /// A parameter specifies the culture and sort rules used in the comparison.
        /// </summary>
        /// <param name="other">Case insensitive string to compare to this instance.</param>
        /// <param name="comparisonType">Value specifying how the case insensitive strings will be compared.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> is case insensitive equivalent of this instance;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public bool Equals(CaseInsensitiveString other, CaseInsensitiveStringComparison comparisonType) =>
            string.Equals(_value, other._value, comparisonType.ToStringComparison());

        /// <summary>
        /// Determines whether this instance of <see cref="CaseInsensitiveString"/> and the <paramref name="other"/> one have the same value ignoring case differences between characters.
        /// A parameter specifies the culture and sort rules used in the comparison.
        /// </summary>
        /// <param name="other">Case insensitive string to compare to this instance.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> is case insensitive equivalent of this instance;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public bool Equals(CaseInsensitiveString other) =>
            string.Equals(_value, other._value, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether this instance of <see cref="CaseInsensitiveString"/> and the <paramref name="other"/> string value have the same value ignoring case differences between characters.
        /// A parameter specifies the culture and sort rules used in the comparison.
        /// </summary>
        /// <param name="other">String to compare to this instance.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> is case insensitive equivalent of this instance;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public bool Equals(string other) =>
            string.Equals(_value, other, StringComparison.OrdinalIgnoreCase);

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is CaseInsensitiveString other && Equals(other)
            || obj is string str && Equals(str);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            new HashCode()
            .Append(_value)
            .CurrentHash;

        #endregion

        #region Operators

        /// <summary>
        /// Determines whether two specified <see cref="CaseInsensitiveString">case insensitive strings</see> have the same value.
        /// </summary>
        /// <param name="left">The first case insensitive strings to compare.</param>
        /// <param name="right">The second case insensitive strings to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(CaseInsensitiveString left, CaseInsensitiveString right) =>
            left.Equals(right);

        /// <summary>
        /// Determines whether two specified <see cref="CaseInsensitiveString">case insensitive strings</see> have different values.
        /// </summary>
        /// <param name="left">The first case insensitive string to compare.</param>
        /// <param name="right">The second case insensitive string to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is different from the value of <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(CaseInsensitiveString left, CaseInsensitiveString right) =>
            !left.Equals(right);

        /// <summary>
        /// Implicitly casts <paramref name="value"/> to string by returning its underlying string value; no actual conversion is performed.
        /// </summary>
        /// <param name="value">Case insensitive string value to cast.</param>
        public static implicit operator string(CaseInsensitiveString value) =>
            value._value;

        /// <summary>
        /// Implicitly casts <paramref name="value"/> to string by returning its underlying string value or null; no actual conversion is performed.
        /// </summary>
        /// <param name="value">Case insensitive string value to cast.</param>
        public static implicit operator string(CaseInsensitiveString? value) =>
            value?._value;

        /// <summary>
        /// Implicitly casts <paramref name="value"/> to case insensitive string by passing the argument value to the new instance of <see cref="CaseInsensitiveString"/>.
        /// </summary>
        /// <param name="value">String value to cast.</param>
        public static implicit operator CaseInsensitiveString(string value) =>
            new CaseInsensitiveString(value);

        #endregion

        #region Static checkers

        /// <summary>
        /// Determines whether two specified <see cref="CaseInsensitiveString"/> objects have the same value ignoring case differences between characters..
        /// A parameter specifies the culture and sort rules used in the comparison.
        /// </summary>
        /// <param name="a">The first case insensitive string to compare, or null.</param>
        /// <param name="b">The second case insensitive string to compare, or null.</param>
        /// <param name="comparisonType">Value specifying how the case insensitive strings will be compared.</param>
        /// <returns>
        /// <see langword="true"/> if both <paramref name="a"/> and <paramref name="b"/> are null or case insensitive equivalents;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool Equals(CaseInsensitiveString? a, CaseInsensitiveString? b, CaseInsensitiveStringComparison comparisonType) =>
            string.Equals(a?._value, b?._value, comparisonType.ToStringComparison());

        /// <summary>
        /// Determines whether given <see cref="CaseInsensitiveString">case insensitive</see> <paramref name="value"/> represents a null or empty string.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>
        /// <see langword="true"/> if underlying string of <paramref name="value"/> is either null or empty;
        /// otherwise <see langword="true"/>.
        /// </returns>
        public static bool IsNullOrEmpty(CaseInsensitiveString value) =>
            string.IsNullOrEmpty(value._value);

        /// <summary>
        /// Determines whether given <see cref="CaseInsensitiveString">case insensitive</see> <paramref name="value"/> represents a null, empty or consisted of white-spaces only string.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>
        /// <see langword="true"/> if underlying string of <paramref name="value"/> is either null or empty or consists of white-spaces only;
        /// otherwise <see langword="true"/>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(CaseInsensitiveString value) =>
            string.IsNullOrWhiteSpace(value._value);

        /// <summary>
        /// Determines whether given nullable <see cref="CaseInsensitiveString">case insensitive</see> <paramref name="value"/> represents a null or empty string.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is null itself or its underlying string is either null or empty;
        /// otherwise <see langword="true"/>.
        /// </returns>
        public static bool IsNullOrEmpty(CaseInsensitiveString? value) =>
            string.IsNullOrEmpty(value?._value);

        /// <summary>
        /// Determines whether given nullable <see cref="CaseInsensitiveString">case insensitive</see> <paramref name="value"/> represents a null, empty or consisted of white-spaces only string.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is null itself or its underlying string is either null or empty or consists of white-spaces only;
        /// otherwise <see langword="true"/>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(CaseInsensitiveString? value) =>
            string.IsNullOrWhiteSpace(value?._value);

        #endregion
    }
}
