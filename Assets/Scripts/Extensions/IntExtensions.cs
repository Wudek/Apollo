using System;


static class IntExtensions
{
    public static int RotateLeft(this int value, int count = 16) { return (value << count) | (value >> (32 - count)); }
}

