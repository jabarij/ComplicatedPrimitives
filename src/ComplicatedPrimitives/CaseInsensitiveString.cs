using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    [System.Diagnostics.DebuggerDisplay("{_value}")]
    public struct CaseInsensitiveString : IEquatable<CaseInsensitiveString>, IEquatable<string>
    {
        public static readonly CaseInsensitiveString Empty = new CaseInsensitiveString(string.Empty);

        private readonly string _value;

        public CaseInsensitiveString(string str)
        {
            _value = str;
        }

        public override string ToString() =>
            _value;

        #region Equality

        public bool Equals(CaseInsensitiveString other, CaseInsensitiveStringComparison comparisonType) =>
            string.Equals(_value, other._value, comparisonType.ToStringComparison());

        public bool Equals(CaseInsensitiveString other) =>
            string.Equals(_value, other._value, StringComparison.OrdinalIgnoreCase);

        public bool Equals(string other) =>
            string.Equals(_value, other, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object obj) =>
            obj is CaseInsensitiveString other && Equals(other)
            || obj is string str && Equals(str);
        public override int GetHashCode() =>
            new HashCode()
            .Append(_value)
            .CurrentHash;

        #endregion

        #region Operators

        public static bool operator ==(CaseInsensitiveString left, CaseInsensitiveString right) =>
            left.Equals(right);

        public static bool operator !=(CaseInsensitiveString left, CaseInsensitiveString right) =>
            !left.Equals(right);

        public static implicit operator string(CaseInsensitiveString value) =>
            value._value;

        public static implicit operator string(CaseInsensitiveString? value) =>
            value?._value;

        public static implicit operator CaseInsensitiveString(string value) =>
            new CaseInsensitiveString(value);

        #endregion

        #region Static checkers

        public static bool Equals(CaseInsensitiveString a, CaseInsensitiveString b, CaseInsensitiveStringComparison comparisonType) =>
            string.Equals(a._value, b._value, comparisonType.ToStringComparison());

        public static bool IsNullOrEmpty(CaseInsensitiveString value) =>
            string.IsNullOrEmpty(value._value);

        public static bool IsNullOrWhiteSpace(CaseInsensitiveString value) =>
            string.IsNullOrWhiteSpace(value._value);

        public static bool IsNullOrEmpty(CaseInsensitiveString? value) =>
            string.IsNullOrEmpty(value?._value);

        public static bool IsNullOrWhiteSpace(CaseInsensitiveString? value) =>
            string.IsNullOrWhiteSpace(value?._value);

        #endregion
    }
}
