using System;

namespace ScriptureMemorizer
{

    class Program
    {
        static void Main(string[] args)
        {
            // Example scripture: John 17: 11-12
            Reference reference = new Reference("Proverbs", 17, 11, 12);
            string text = "And now I am no more in the world, but these are in the world, and I come to thee. Holy Father, keep through thine own name those whom thou hast given me, that they may be one, as we are. " +
                          "While I was with them in the world, I kept them in thy name: those that thou gavest me I have kept, and none of them is lost, but the son of perdition; that the scripture might be fulfilled.";

            Scripture scripture = new Scripture(reference, text);

            // Main loop
            while (true)
            {
                Console.Clear();
                scripture.Display();

                Console.WriteLine("\nPress Enter to hide words or type 'quit' to end:");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                    break;

                if (scripture.AllWordsHidden())
                {
                    Console.WriteLine("\nAll words are hidden. Program ending...");
                    break;
                }

                scripture.HideRandomWords();
            }
        }
    }
}
