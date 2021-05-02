using System;

namespace ComplicatedPrimitives.TestAbstractions
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class RequirementAttribute : Attribute
    {
    }
}
