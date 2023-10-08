namespace BaseNumberApp.Models;

public record Number(int Base, int DecimalNumber, string Digits)
{
  public override string ToString()
  {
    return DecimalToArbitrarySystem(this.DecimalNumber, this.Base);
  }

  private string DecimalToArbitrarySystem(long decimalNumber, int radix)
  {
    const int BitsInLong = 64;

    // if (radix < 2 || radix > Digits.Length)
    //   throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length);

    if (decimalNumber == 0)
      return "0";

    var index = BitsInLong - 1;
    var currentNumber = Math.Abs(decimalNumber);
    var charArray = new char[BitsInLong];

    while (currentNumber != 0)
    {
      var remainder = (int)(currentNumber % radix);
      charArray[index--] = Digits[remainder];
      currentNumber /= radix;
    }

    var result = new String(charArray, index + 1, BitsInLong - index - 1);

    if (decimalNumber < 0)
    {
      result = "-" + result;
    }

    return result;
  }

  public int ToInt(string value)
  {
    var intValue = 0;
    var intPower = 1;

    foreach(var c in value.ToCharArray().Reverse())
    {
      var intPosValue = Digits.IndexOf(c);
      intValue += intPosValue * intPower;
      intPower *= Digits.Length;
    }

    return intValue;
  }
}
