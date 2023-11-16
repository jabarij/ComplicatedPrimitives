using System;
using System.Runtime.Serialization;

namespace ComplicatedPrimitives;

[Serializable]
public class ParsingException : FormatException
{
    public const string DefaultMessage = "Incorrect format.";

    public ParsingException(string? format, string message = DefaultMessage, Exception? inner = default)
        : base(message, inner)
    {
        Format = format;
    }
    
#if NET8_0_OR_GREATER
    [Obsolete(DiagnosticId = "SYSLIB0051")]
#endif
    protected ParsingException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Format = (string?)info.GetValue(nameof(Format), typeof(string));
    }

    public string? Format { get; private set; }

#if NET8_0_OR_GREATER
    [Obsolete(DiagnosticId = "SYSLIB0051")]
#endif
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Format), Format);
    }
}