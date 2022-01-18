namespace ComplicatedPrimitives.Validation
{
    public static class ErrorMessages
    {
        public const string UnknownFormat = "Unknown format.";
        public static string StringTooLong(int maxLength) =>
            string.Format("String must be at most {0} chars long.", maxLength);
    }
}