using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction frac1 = new Fraction();
        Console.WriteLine($"Default fraction: {frac1.GetFractionString()}");
        Console.WriteLine($"Decimal value: {frac1.GetDecimalValue()}");

        Fraction frac2 = new Fraction(5);
        Console.WriteLine($"\nFraction with numerator only: {frac2.GetFractionString()}");
        Console.WriteLine($"Decimal value: {frac2.GetDecimalValue()}");

        Fraction frac3 = new Fraction(3, 4);
        Console.WriteLine($"\nFraction with numerator and denominator: {frac3.GetFractionString()}");
        Console.WriteLine($"Decimal value: {frac3.GetDecimalValue()}");

        Fraction frac4 = new Fraction(1, 3);
        Console.WriteLine($"\nAnother fraction with numerator and denominator: {frac4.GetFractionString()}");
        Console.WriteLine($"Decimal value: {frac4.GetDecimalValue()}");
    }
}