using System;
using UnityEngine;

internal static class Vector3Extensions {
    public static Vector3 Clone(this Vector3 vector) {
        return new Vector3(vector.x, vector.y, vector.z);
    }

    public static bool EqualsAlmost(this Vector3 vector, Vector3 otherVector) {
        return vector.x.AlmostEquals(otherVector.x) && vector.y.AlmostEquals(otherVector.y) &&
               vector.z.AlmostEquals(otherVector.z);
    }

    public static bool SameDirectionAs(this Vector3 vector, Vector3 otherVector) {
        return Math.Sign(vector.x) == Math.Sign(otherVector.x) && Math.Sign(vector.y) == Math.Sign(otherVector.y) &&
               Math.Sign(vector.z) == Math.Sign(otherVector.z);
    }

    public static Vector3 ClearX(this Vector3 vector) {
        return new Vector3(0, vector.y, vector.z);
    }

    public static Vector3 ClearY(this Vector3 vector) {
        return new Vector3(vector.x, 0, vector.z);
    }

    public static Vector3 ClearZ(this Vector3 vector) {
        return new Vector3(vector.x, vector.y, 0);
    }

    public static Vector3 ChangeX(this Vector3 vector, float newValue) {
        return new Vector3(newValue, vector.y, vector.z);
    }
    public static Vector3 ChangeY(this Vector3 vector, float newValue)
    {
        return new Vector3(vector.x, newValue, vector.z);
    }
    public static Vector3 ChangeZ(this Vector3 vector, float newValue)
    {
        return new Vector3(vector.x, vector.y, newValue);
    }
}