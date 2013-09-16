using System;

public static class RandomGenerator {
    private static object locker = new object();
    private static Random seedGenerator = new Random(Environment.TickCount);

    public static Random Rng {
        get {
            return new Random(GetSeed());
        }
    }

    private static int GetSeed() {
        lock (locker) {
            return seedGenerator.Next(int.MinValue, int.MaxValue);
        }
    }

    public static uint Next(this Random random, uint max) {
        return  (uint) (random.NextDouble() * max);
    }

    public static byte NextByte(this Random random) {
        Byte[] filler = new byte[1];
        random.NextBytes(filler);
        return filler[0];
    }

}