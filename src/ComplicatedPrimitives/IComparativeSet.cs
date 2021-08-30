namespace ComplicatedPrimitives
{
    /// <summary>
    /// Exposes interface providing mathematical set relations and operations.
    /// </summary>
    /// <typeparam name="TSet">Type of set to handle.</typeparam>
    /// <typeparam name="T">Type of set's element.</typeparam>
    public interface IComparativeSet<in TSet, T>
    {
        /// <summary>
        /// Gets the value indicating whether this <see cref="IComparativeSet{TSet, T}">set</see> contains given <paramref name="value"/>.
        /// This function is equivalent of mathematical expression:
        /// <code>x ∊ A</code>
        /// where:
        /// <list type="bullet">
        /// <item><term>A</term><description>this instance of <see cref="IComparativeSet{TSet, T}"/>,</description></item>
        /// <item><term>x</term><description><paramref name="value"/>.</description></item>
        /// </list>
        /// </summary>
        /// <param name="value">Value to check inclusion relation of.</param>
        /// <returns><see langword="true"/> if the <paramref name="value"/> belongs to this set; otherwise <see langword="false"/>.</returns>
        bool Contains(T value);

        /// <summary>
        /// Gets the value indicating whether this <see cref="IComparativeSet{TSet, T}">set</see> intersects (has common elements) with the <paramref name="other"/> set.
        /// This function is equivalent of mathematical expression:
        /// <code>A ∩ B ≠ ∅</code>
        /// where:
        /// <list type="bullet">
        /// <item><term>A</term><description>this instance of <see cref="IComparativeSet{TSet, T}"/>,</description></item>
        /// <item><term>B</term><description><paramref name="other"/>.</description></item>
        /// </list>
        /// </summary>
        /// <param name="other">Other set to check intersection relation with.</param>
        /// <returns><see langword="true"/> if the <paramref name="other"/> has common elements with this set; otherwise <see langword="false"/>.</returns>
        bool IntersectsWith(TSet other);

        /// <summary>
        /// Gets the value indicating whether this <see cref="IComparativeSet{TSet, T}">set</see> is a subset of the <paramref name="other"/> set.
        /// This function is equivalent of mathematical expression:
        /// <code>A ⊆ B</code>
        /// where:
        /// <list type="bullet">
        /// <item><term>A</term><description>this instance of <see cref="IRange{TSet, T}"/>,</description></item>
        /// <item><term>B</term><description><paramref name="other"/>.</description></item>
        /// </list>
        /// </summary>
        /// <param name="other">Set to check inclusion relation with.</param>
        /// <returns>
        /// <see langword="true"/> if this set is a subset of the <paramref name="other"/> one; otherwise <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// This function checks the weak inclusion relation which means that a set is in a given relation with itself (or equal set).
        /// To check strict version of this relation (excluding equal sets), use <see cref="IsProperSubsetOf(TSet)"/>.
        /// </remarks>
        bool IsSubsetOf(TSet other);

        /// <summary>
        /// Gets the value indicating whether this <see cref="IComparativeSet{TSet, T}">set</see> is a proper subset of the <paramref name="other"/> set.
        /// This function is equivalent of mathematical expression:
        /// <code>A ⊂ B</code>
        /// where:
        /// <list type="bullet">
        /// <item><term>A</term><description>this instance of <see cref="IComparativeSet{TSet, T}"/>,</description></item>
        /// <item><term>B</term><description><paramref name="other"/>.</description></item>
        /// </list>
        /// </summary>
        /// <param name="other">Set to check strict inclusion relation with.</param>
        /// <returns>
        /// <see langword="true"/> if this set is a proper subset of the <paramref name="other"/> one; otherwise <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// This function checks the strict inclusion relation which means that a set is not in a given relation with itself (or equal set).
        /// To check weak version of this relation (excluding equal sets), use <see cref="IsSubsetOf(TSet)"/>.
        /// </remarks>
        bool IsProperSubsetOf(TSet other);

        /// <summary>
        /// Gets the value indicating whether this <see cref="IComparativeSet{TSet, T}">set</see> is a superset of the <paramref name="other"/> set.
        /// This function is equivalent of mathematical expression:
        /// <code>A ⊇ B</code>
        /// where:
        /// <list type="bullet">
        /// <item><term>A</term><description>this instance of <see cref="IRange{TSet, T}"/>,</description></item>
        /// <item><term>B</term><description><paramref name="other"/>.</description></item>
        /// </list>
        /// </summary>
        /// <param name="other">Set to check inclusion relation with.</param>
        /// <returns>
        /// <see langword="true"/> if this set is a superset of the <paramref name="other"/> one; otherwise <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// This function checks the weak inclusion relation which means that a set is in a given relation with itself (or equal set).
        /// To check strict version of this relation (excluding equal sets), use <see cref="IsProperSupersetOf(TSet)"/>.
        /// </remarks>
        bool IsSupersetOf(TSet other);

        /// <summary>
        /// Gets the value indicating whether this <see cref="IComparativeSet{TSet, T}">set</see> is a proper superset of the <paramref name="other"/> set.
        /// This function is equivalent of mathematical expression:
        /// <code>A ⊃ B</code>
        /// where:
        /// <list type="bullet">
        /// <item><term>A</term><description>this instance of <see cref="IComparativeSet{TSet, T}"/>,</description></item>
        /// <item><term>B</term><description><paramref name="other"/>.</description></item>
        /// </list>
        /// </summary>
        /// <param name="other">Set to check strict inclusion relation with.</param>
        /// <returns>
        /// <see langword="true"/> if this set is a proper superset of the <paramref name="other"/> one; otherwise <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// This function checks the strict inclusion relation which means that a set is not in a given relation with itself (or equal set).
        /// To check weak version of this relation (excluding equal sets), use <see cref="IsSupersetOf(TSet)"/>.
        /// </remarks>
        bool IsProperSupersetOf(TSet other);
    }
}
