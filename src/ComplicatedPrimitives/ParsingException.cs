using System;
using System.Runtime.Serialization;

namespace ComplicatedPrimitives
{
    /// <summary>
    /// The exception that is thrown when the <see cref="IParser{T}.Parse(string)"/> failed because of incorrect format.
    /// </summary>
    [Serializable]
    public class ParsingException : FormatException
    {
        /// <summary>
        /// Default message for this exception.
        /// </summary>
        public const string DefaultMessage = "Incorrect format.";

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class with a specified incorrect <paramref name="format"/>,
        /// error <paramref name="message"/> and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="format">Incorrect format value that caused this exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException parameter is not a null reference
        /// (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception.
        /// </param>
        public ParsingException(string format, string message, Exception inner) : base(message, inner)
        {
            Format = format;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class with a specified incorrect <paramref name="format"/>
        /// and error <paramref name="message"/>.
        /// </summary>
        /// <param name="format">Incorrect format value that caused this exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ParsingException(string format, string message) : this(format, message, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class with a specified incorrect <paramref name="format"/>.
        /// </summary>
        /// <param name="format">Incorrect format value that caused this exception.</param>
        public ParsingException(string format) : this(format, DefaultMessage) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected ParsingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Format = (string)info.GetValue(nameof(Format), typeof(string));
        }

        /// <summary>
        /// Gets the incorrect format value that caused this exception.
        /// </summary>
        public string Format { get; private set; }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Format), Format);
        }
    }
}
