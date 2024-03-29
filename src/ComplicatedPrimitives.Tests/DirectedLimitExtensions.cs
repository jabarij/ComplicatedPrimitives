﻿using System;

namespace ComplicatedPrimitives.Tests;

public static class DirectedLimitExtensions
{
    public static DirectedLimit<T> With<T>(
        this DirectedLimit<T> directedLimit,
        T? value = null,
        LimitType? type = null,
        LimitSide? side = null)
        where T : struct, IComparable<T> =>
        new(
            limitValue: new LimitValue<T>(
                value: value ?? directedLimit.Value,
                type: type ?? directedLimit.Type),
            side: side ?? directedLimit.Side);

    public static DirectedLimit<T> FlipLimitType<T>(this DirectedLimit<T> limit) where T : IComparable<T> =>
        new(limit.LimitValue.FlipLimitType(), limit.Side);
}