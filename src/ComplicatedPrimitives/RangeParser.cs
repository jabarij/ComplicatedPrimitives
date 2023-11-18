using System;

namespace ComplicatedPrimitives;

public readonly struct RangeParser<T> : IRangeParser<T>
    where T : IComparable<T>
{
    private const char DefaultSeparator = ';';
    private const string SimpleInfinityDescriptor = "oo";
    private const string SophisticatedInfinityDescriptor = "∞";

    private readonly IParser<T> _valueParser;

    public char Separator { get; }

    public RangeParser(IParser<T> valueParser, char separator = DefaultSeparator)
    {
        _valueParser = valueParser ?? throw new ArgumentNullException(nameof(valueParser));
        Separator = separator;
    }

    public Range<T> Parse(ReadOnlySpan<char> str)
    {
        if (str.IsEmpty || str.IsWhiteSpace())
            throw new ArgumentException("Given format is empty or consists of white-spaces only.", nameof(str));

        if (!TryParseLimit(str[0], out var leftLimit)
            || leftLimit.side != LimitSide.Left)
            throw new ParsingException(str[0].ToString(), "Unrecognized left limit descriptor.");

        if (!TryParseLimit(str[^1], out var rightLimit)
            || rightLimit.side != LimitSide.Right)
            throw new ParsingException(str[^1].ToString(), "Unrecognized right limit descriptor.");

        var innerSpan = str[1..^1];
        
        var separatorIndex = innerSpan.IndexOf(Separator);
        if (separatorIndex == -1)
            throw new ParsingException(new string(innerSpan), $"Separator '{Separator}' not found.");
        
        var leftValueSpan = innerSpan[..separatorIndex];
        var rightValueSpan = innerSpan[(separatorIndex + 1)..];

        var leftLimitValue = IsNegativeInfinityDescriptor(leftValueSpan)
            ? LimitValue<T>.Infinity
            : new LimitValue<T>(_valueParser.Parse(leftValueSpan), type: leftLimit.type);

        var rightLimitValue = IsPositiveInfinityDescriptor(rightValueSpan)
            ? LimitValue<T>.Infinity
            : new LimitValue<T>(_valueParser.Parse(rightValueSpan), type: rightLimit.type);

        return new Range<T>(
            left: leftLimitValue,
            right: rightLimitValue);
    }

    public bool TryParse(ReadOnlySpan<char> str, out Range<T> range)
    {
        if (str.IsEmpty || str.IsWhiteSpace())
        {
            range = default;
            return false;
        }

        if (str.Length <= 3)
        {
            range = default;
            return false;
        }

        if (!TryParseLimit(str[0], out var leftLimit)
            || leftLimit.side != LimitSide.Left)
        {
            range = default;
            return false;
        }

        if (!TryParseLimit(str[^1], out var rightLimit)
            || rightLimit.side != LimitSide.Right)
        {
            range = default;
            return false;
        }
        
        var innerSpan = str[1..^1];
        
        var separatorIndex = innerSpan.IndexOf(Separator);
        if (separatorIndex == -1)
        {
            range = default;
            return false;
        }

        var leftValueSpan = innerSpan[..separatorIndex];
        var rightValueSpan = innerSpan[(separatorIndex + 1)..];

        LimitValue<T> leftLimitValue;
        if (IsNegativeInfinityDescriptor(leftValueSpan))
            leftLimitValue = LimitValue<T>.Infinity;
        else if (_valueParser.TryParse(leftValueSpan, out var leftValue))
            leftLimitValue = new LimitValue<T>(leftValue, type: leftLimit.type);
        else
        {
            range = default;
            return false;
        }

        LimitValue<T> rightLimitValue;
        if (IsPositiveInfinityDescriptor(rightValueSpan))
            rightLimitValue = LimitValue<T>.Infinity;
        else if (_valueParser.TryParse(rightValueSpan, out var rightValue))
            rightLimitValue = new LimitValue<T>(rightValue, type: rightLimit.type);
        else
        {
            range = default;
            return false;
        }

        range = new Range<T>(
            left: leftLimitValue,
            right: rightLimitValue);
        return true;
    }

    private static bool IsNegativeInfinityDescriptor(ReadOnlySpan<char> value)
    {
        if (value[0] == '-')
            value = value[1..];
        
        ReadOnlySpan<char> simpleInfinityDescriptor = SimpleInfinityDescriptor;
        ReadOnlySpan<char> sophisticatedInfinityDescriptor = SophisticatedInfinityDescriptor;
        return value.Equals(simpleInfinityDescriptor, StringComparison.OrdinalIgnoreCase)
               || value.Equals(sophisticatedInfinityDescriptor, StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsPositiveInfinityDescriptor(ReadOnlySpan<char> value)
    {
        if (value[0] == '+')
            value = value[1..];
        
        ReadOnlySpan<char> simpleInfinityDescriptor = SimpleInfinityDescriptor;
        ReadOnlySpan<char> sophisticatedInfinityDescriptor = SophisticatedInfinityDescriptor;
        return value.Equals(simpleInfinityDescriptor, StringComparison.OrdinalIgnoreCase)
               || value.Equals(sophisticatedInfinityDescriptor, StringComparison.OrdinalIgnoreCase);
    }

    private static bool TryParseLimit(char limitChar, out (LimitType type, LimitSide side) result)
    {
        switch (limitChar)
        {
            case '(':
                result = (LimitType.Open, LimitSide.Left);
                return true;
            case '[':
                result = (LimitType.Closed, LimitSide.Left);
                return true;
            case ']':
                result = (LimitType.Closed, LimitSide.Right);
                return true;
            case ')':
                result = (LimitType.Open, LimitSide.Right);
                return true;
            default:
                result = default((LimitType, LimitSide));
                return false;
        }
    }
}