using System;

internal static class FloatExtensions
{
    public static bool AlmostEquals(this float pFloat1, float pFloat2, float pPrecision = 0.00001f)
    {
        return (Math.Abs(pFloat1 - pFloat2) <= pPrecision);
    }

    public static bool IsZero(this float pFloat)
    {
        return AlmostEquals(pFloat, 0f);
    }

    public static bool IsNotZero(this float pFloat)
    {
        return !IsZero(pFloat);
    }

    /// <summary>
    ///     Applying padding to a float means that if it's less than the padding, the float is ignored and 0 is used instead.
    /// </summary>
    /// <param name="pFloat1"></param>
    /// <param name="pPadding"></param>
    /// <returns></returns>
    public static float ApplyPadding(this float pFloat1, float pPadding = 0.00001f)
    {
        return Math.Abs(pFloat1) < pPadding ? 0f : pFloat1;
    }
}