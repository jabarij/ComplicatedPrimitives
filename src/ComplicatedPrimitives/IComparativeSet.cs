namespace ComplicatedPrimitives
{
    /// <include
    ///   file='ComplicatedPrimitives.xml'
    ///   path='//member[@name="T:ComplicatedPrimitives.IComparativeSet`2"]' />
    public interface IComparativeSet<in TSet, T>
    {
        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.IComparativeSet`2.Contains(`1)"]' />
        bool Contains(T value);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.IComparativeSet`2.IntersectsWith(`0)"]' />
        bool IntersectsWith(TSet other);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.IComparativeSet`2.IsSubsetOf(`0)"]' />
        bool IsSubsetOf(TSet other);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.IComparativeSet`2.IsProperSubsetOf(`0)"]' />
        bool IsProperSubsetOf(TSet other);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.IComparativeSet`2.IsSupersetOf(`0)"]' />
        bool IsSupersetOf(TSet other);

        /// <include
        ///   file='ComplicatedPrimitives.xml'
        ///   path='//member[@name="M:ComplicatedPrimitives.IComparativeSet`2.IsProperSupersetOf(`0)"]' />
        bool IsProperSupersetOf(TSet other);
    }
}
