using System;
using System.Runtime.Serialization;

namespace ComplicatedPrimitives
{
    [Serializable]
    public class ParsingException : FormatException
    {
        public const string DefaultMessage = "Incorrect format.";

        public ParsingException(string format, string message, Exception inner) : base(message, inner)
        {
            Format = format;
        }
        public ParsingException(string format, string message) : this(format, message, null) { }
        public ParsingException(string format) : this(format, DefaultMessage) { }
        protected ParsingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Format = (string)info.GetValue(nameof(Format), typeof(string));
        }

        public string Format { get; private set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Format), Format);
        }
    }
}
