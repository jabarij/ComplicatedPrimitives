using DotNetExtensions;
using System;

namespace ComplicatedPrimitives
{
    /// <include
    ///   file='ComplicatedPrimitives.xml'
    ///   path='//member[@name="T:ComplicatedPrimitives.CaseInsensitiveString"]' />
    [System.Diagnostics.DebuggerDisplay("{_value}")]
    public struct CaseInsensitiveString : IEquatable<CaseInsensitiveString>, IEquatable<string>
    {
        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="F:ComplicatedPrimitives.CaseInsensitiveString.Empty"]' />
        public static readonly CaseInsensitiveString Empty = new CaseInsensitiveString(string.Empty);

        private readonly string _value;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.#ctor(System.String)"]' />
        public CaseInsensitiveString(string str) => _value = str;

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.ToString"]' />
        public override string ToString() => _value;

        #region Equality

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.Equals(ComplicatedPrimitives.CaseInsensitiveString,ComplicatedPrimitives.CaseInsensitiveStringComparison)"]' />
        public bool Equals(CaseInsensitiveString other, CaseInsensitiveStringComparison comparisonType) =>
            string.Equals(_value, other._value, comparisonType.ToStringComparison());

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.Equals(ComplicatedPrimitives.CaseInsensitiveString)"]' />
        public bool Equals(CaseInsensitiveString other) =>
            string.Equals(_value, other._value, StringComparison.OrdinalIgnoreCase);

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.Equals(System.String)"]' />
        public bool Equals(string other) =>
            string.Equals(_value, other, StringComparison.OrdinalIgnoreCase);

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.Equals(System.Object)"]' />
        public override bool Equals(object obj) =>
            obj is CaseInsensitiveString other && Equals(other)
            || obj is string str && Equals(str);

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.GetHashCode"]' />
        public override int GetHashCode() =>
            new HashCode()
            .Append(_value)
            .CurrentHash;

        #endregion

        #region Operators

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.op_Equality(ComplicatedPrimitives.CaseInsensitiveString,ComplicatedPrimitives.CaseInsensitiveString)"]' />
        public static bool operator ==(CaseInsensitiveString left, CaseInsensitiveString right) =>
            left.Equals(right);

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.op_Inequality(ComplicatedPrimitives.CaseInsensitiveString,ComplicatedPrimitives.CaseInsensitiveString)"]' />
        public static bool operator !=(CaseInsensitiveString left, CaseInsensitiveString right) =>
            !left.Equals(right);

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.op_Implicit(ComplicatedPrimitives.CaseInsensitiveString)~System.String"]' />
        public static implicit operator string(CaseInsensitiveString value) =>
            value._value;

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.op_Implicit(System.Nullable{ComplicatedPrimitives.CaseInsensitiveString})~System.String"]' />
        public static implicit operator string(CaseInsensitiveString? value) =>
            value?._value;

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.op_Implicit(System.String)~ComplicatedPrimitives.CaseInsensitiveString"]' />
        public static implicit operator CaseInsensitiveString(string value) =>
            new CaseInsensitiveString(value);

        #endregion

        #region Static checkers

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.Equals(System.Nullable{ComplicatedPrimitives.CaseInsensitiveString},System.Nullable{ComplicatedPrimitives.CaseInsensitiveString},ComplicatedPrimitives.CaseInsensitiveStringComparison)"]' />
        public static bool Equals(CaseInsensitiveString? a, CaseInsensitiveString? b, CaseInsensitiveStringComparison comparisonType) =>
            string.Equals(a?._value, b?._value, comparisonType.ToStringComparison());

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.IsNullOrEmpty(ComplicatedPrimitives.CaseInsensitiveString)"]' />
        public static bool IsNullOrEmpty(CaseInsensitiveString value) =>
            string.IsNullOrEmpty(value._value);

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.IsNullOrWhiteSpace(ComplicatedPrimitives.CaseInsensitiveString)"]' />
        public static bool IsNullOrWhiteSpace(CaseInsensitiveString value) =>
            string.IsNullOrWhiteSpace(value._value);

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.IsNullOrEmpty(System.Nullable{ComplicatedPrimitives.CaseInsensitiveString})"]' />
        public static bool IsNullOrEmpty(CaseInsensitiveString? value) =>
            string.IsNullOrEmpty(value?._value);

        /// <include
		///   file='ComplicatedPrimitives.xml'
		///   path='//member[@name="M:ComplicatedPrimitives.CaseInsensitiveString.IsNullOrWhiteSpace(System.Nullable{ComplicatedPrimitives.CaseInsensitiveString})"]' />
        public static bool IsNullOrWhiteSpace(CaseInsensitiveString? value) =>
            string.IsNullOrWhiteSpace(value?._value);

        #endregion
    }
}
