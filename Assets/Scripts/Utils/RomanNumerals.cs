using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RomanNumeralConverter {
    private readonly int arabic;

    public RomanNumeralConverter(int arabic) {
        this.arabic = arabic;
    }

    public static string Convert(string arabicOrRomanNumeral) {
        int arabic = int.Parse(arabicOrRomanNumeral);
        return Convert(arabic);
    }

    public static string Convert(int arabic)
    {
        if(arabic < 1) {
            throw new ArgumentOutOfRangeException("Input cannot be less than 1");
        }
        return
            new RomanNumeralConverter(arabic).ToRomanNumerals().Aggregate(new StringBuilder(),
                                                                          (sb, romanNumeral) =>
                                                                          sb.Append(romanNumeral.Numeral)).Replace(" ", "").ToString();
    }

    public IList<RomanNumeral> ToRomanNumerals() {
        var romanNumerals = new List<RomanNumeral>();
        int number = arabic;

        while (number > 0) {
            RomanNumeral romanNumeral =
                (from numeral in RomanNumerals.All where numeral.Matches(number) != null select numeral.Matches(number))
                    .First();
            number -= romanNumeral.ArabicValue;
            romanNumerals.Add(romanNumeral);
        }
        return romanNumerals;
    }
}

public class RomanNumerals {
    public static readonly RomanNumeral I = new RomanNumeral("I", 1);
    public static readonly RomanNumeral V = new RomanNumeral("V", 5, I);
    public static readonly RomanNumeral X = new RomanNumeral("X", 10, I);
    public static readonly RomanNumeral L = new RomanNumeral("L", 50, X);
    public static readonly RomanNumeral C = new RomanNumeral("C", 100, X);
    public static readonly RomanNumeral D = new RomanNumeral("D", 500, C);
    public static readonly RomanNumeral M = new RomanNumeral("M", 1000, C);

    public static readonly IEnumerable<RomanNumeral> All = new List<RomanNumeral> {M, D, C, L, X, V, I};
}

public class RomanNumeral {
    public readonly int ArabicValue;
    public readonly string Numeral;
    private readonly RomanNumeral canBeModifiedBy;

    public RomanNumeral(string numeral, int arabicValue)
        : this(numeral, arabicValue, null) {}

    public RomanNumeral(string numeral, int arabicValue, RomanNumeral canBeModifiedBy) {
        Numeral = numeral;
        ArabicValue = arabicValue;
        this.canBeModifiedBy = canBeModifiedBy;
    }

    public RomanNumeral Matches(int arabicValue) {
        if (ArabicValue <= arabicValue)
            return this;

        if (canBeModifiedBy != null && ArabicValue - canBeModifiedBy.ArabicValue <= arabicValue)
            return new RomanNumeral(canBeModifiedBy.Numeral + Numeral, ArabicValue - canBeModifiedBy.ArabicValue);

        return null;
    }
}