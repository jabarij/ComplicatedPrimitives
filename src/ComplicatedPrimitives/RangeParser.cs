using System;

namespace ComplicatedPrimitives;

public class RangeParser<T> : IRangeParser<T>
    where T : IComparable<T>
{
    public const char Separator = ';';
    public const string SimpleInfinityDescriptor = "oo";
    public const string SophisticatedInfinityDescriptor = "∞";

    private readonly IParser<T> _parseValue;

    public RangeParser(IParser<T> valueParser)
    {
        _parseValue = valueParser ?? throw new ArgumentNullException(nameof(valueParser));
    }

    public Range<T> Parse(string str)
    {
        if (string.IsNullOrEmpty(str))
            throw new ArgumentNullException(nameof(str));
        if (str.Length < 3)
            throw new ParsingException(str, "Too short value.");

        if (!TryParseLimit(str[0], out var leftLimit)
            || leftLimit.side != LimitSide.Left)
            throw new ParsingException(str[0].ToString(), "Unrecognized left limit descriptor.");

        if (!TryParseLimit(str[str.Length - 1], out var rightLimit)
            || rightLimit.side != LimitSide.Right)
            throw new ParsingException(str[str.Length - 1].ToString(), "Unrecognized right limit descriptor.");

        int separatorIndex = str.IndexOf(Separator, 1);
        if (separatorIndex == -1)
            throw new ParsingException(str, $"Separator '{Separator}' not found.");

        string[] values = str
            .Substring(1, str.Length - 2)
            .Split(new[] { Separator }, 2);

        var leftValueStr = values[0];
        var leftLimitValue = IsInifinityDescriptor(leftValueStr)
            ? LimitValue<T>.Infinity
            : new LimitValue<T>(_parseValue.Parse(leftValueStr), type: leftLimit.type);

        var rightValueStr = values[1];
        var rightLimitValue = IsInifinityDescriptor(rightValueStr)
            ? LimitValue<T>.Infinity
            : new LimitValue<T>(_parseValue.Parse(rightValueStr), type: rightLimit.type);

        return new Range<T>(
            left: leftLimitValue,
            right: rightLimitValue);
    }

    private static bool IsInifinityDescriptor(string leftValueStr)
    {
        return string.Equals(SimpleInfinityDescriptor, leftValueStr, StringComparison.OrdinalIgnoreCase)
               || string.Equals(SophisticatedInfinityDescriptor, leftValueStr, StringComparison.OrdinalIgnoreCase);
    }

    public bool TryParse(string str, out Range<T> range)
    {
        if (string.IsNullOrEmpty(str))
        {
            range = default;
            return false;
        }

        if (str.Length < 3)
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

        if (!TryParseLimit(str[str.Length - 1], out var rightLimit)
            || rightLimit.side != LimitSide.Right)
        {
            range = default;
            return false;
        }

        int separatorIndex = str.IndexOf(Separator, 1);
        if (separatorIndex == -1)
        {
            range = default;
            return false;
        }

        string[] values = str
            .Substring(1, str.Length - 2)
            .Split(new[] { Separator }, 2);

        LimitValue<T> leftLimitValue;
        var leftValueStr = values[0];
        if (IsInifinityDescriptor(leftValueStr))
            leftLimitValue = LimitValue<T>.Infinity;
        else if (_parseValue.TryParse(leftValueStr, out var leftValue))
            leftLimitValue = new LimitValue<T>(leftValue, type: leftLimit.type);
        else
        {
            range = default;
            return false;
        }

        LimitValue<T> rightLimitValue;
        var rightValueStr = values[1];
        if (IsInifinityDescriptor(rightValueStr))
            rightLimitValue = LimitValue<T>.Infinity;
        else if (_parseValue.TryParse(rightValueStr, out var rightValue))
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