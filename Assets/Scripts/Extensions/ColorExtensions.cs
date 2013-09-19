
using System;
using UnityEngine;

internal static class ColorExtensions
{
    
    /// <summary>
    /// Converts from Color to Color32.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Color32 ToColor32(this Color color)
    {
        return new Color32(color.r.ToByte(), color.g.ToByte(), color.b.ToByte(), color.a.ToByte());
    }         
}
