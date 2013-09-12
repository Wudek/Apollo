using System;

internal static class RandomExtensions
{
    /// <summary>
    /// Gets the next bool based off of a margin.
    /// </summary>
    /// <param name="pRandom"> </param>
    /// <param name="pMargin">Value between 0f and 1f, the higher it is, the more likely true will come out.</param>
    /// <returns></returns>
    public static bool NextBool(this Random pRandom, float pMargin = 0.5f) {
        pMargin = pMargin.Clamp(0f, 1f);
        return pRandom.NextDouble() < pMargin;
    }
}
