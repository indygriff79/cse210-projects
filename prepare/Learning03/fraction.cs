using System;

class Fraction
{
    private int numerator;
    private int denominator;

    // Default constructor (1/1)
    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }
    // Constructor with numerator only (numerator/1)
    public Fraction(int num)
    {
        numerator = num;
        denominator = 1;
    }
    // Constructor with numerator and denominator (numerator/denominator)
    public Fraction(int num, int denom)
    {
        numerator = num;
        denominator = denom;
    }
    // Method to get the fraction as a string
    public string GetFractionString()
    {
        return $"{numerator}/{denominator}";
    
    }
    // Method to get the decimal value of the fraction
    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }

}