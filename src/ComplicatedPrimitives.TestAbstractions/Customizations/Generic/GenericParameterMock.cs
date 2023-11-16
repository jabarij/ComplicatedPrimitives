using AutoFixture.Kernel;

namespace ComplicatedPrimitives.TestAbstractions.Customizations.Generic;

public class GenericParameterMock
{
    private GenericParameterMock() { }

    public T? ResolveExact<T>(ISpecimenContext context) => default;
    public T? UseValue<T>(T value) => default;
}