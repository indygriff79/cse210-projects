using System;

class Program
{
    static void Main(string[] args)
    {
        Assignment a1 = new Assignment("Sam Richards", "Multiplication");
        Console.WriteLine(a1.GetSummary());

        MathAssignment a2 = new MathAssignment("Julia Dunn", "Division", "2.3", "5-15");
        Console.WriteLine(a2.GetSummary());
        Console.WriteLine(a2.GetHomeworkList());

        WritingAssignment a3 = new WritingAssignment("Charles Smith", "European  History", "The Causes of World War II");
        Console.WriteLine(a3.GetSummary());
        Console.WriteLine(a3.GetWritingInformation());



    }
}