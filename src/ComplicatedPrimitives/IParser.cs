namespace ComplicatedPrimitives
{
    /// <summary>
    /// Defines a generalized parsing methods that a value type or class implements to restore an instance of <typeparamref name="T"/> from its string equivalent.
    /// </summary>
    /// <typeparam name="T">The type of object to parse from string.</typeparam>
    public interface IParser<T>
    {
        /// <summary>
        /// Converts the specified <paramref name="str"/> to the instance of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="str">A string that contains an object to convert.</param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> that is equivalent to the value contained in <paramref name="str"/>.
        /// </returns>
        T Parse(string str);

        /// <summary>
        /// Tries to convert the specified <paramref name="str"/> to the instance of <typeparamref name="T"/>, and returns a value indicating whether the conversion succeeded.
        /// </summary>
        /// <param name="str">A string that contains an object to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the instance of <typeparamref name="T"/> equivalent to the value contained in <paramref name="str"/> if the conversion succeeded,
        /// or <c>default(<typeparamref name="T"/>)</c> if the conversion failed.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="str"/> was converted successfully; otherwise, <see langword="false"/>.
        /// </returns>
        bool TryParse(string str, out T result);
    }
}