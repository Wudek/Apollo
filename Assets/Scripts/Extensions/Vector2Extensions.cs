using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

internal static class Vector2Extensions {
    public static bool AlmostEquals(this Vector2 pVectorA, Vector2 pVectorB) {
        return pVectorA.x.AlmostEquals(pVectorB.x) && pVectorA.y.AlmostEquals(pVectorB.y);
    }
}

