using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    class Scripture
    {
        private Reference _reference;
        private List<Word> _words;
        private Random _random;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text.Split(' ').Select(word => new Word(word)).ToList();
            _random = new Random();
        }

        public void Display()
        {
            Console.WriteLine(_reference.GetDisplayText());
            foreach (Word word in _words)
            {
                Console.Write(word.GetDisplayText() + " ");
            }
            Console.WriteLine();
        }

        public void HideRandomWords()
        {
            int wordsToHide = _random.Next(2, 4);
            for (int i = 0; i < wordsToHide; i++)
            {
                int index = _random.Next(_words.Count);
                _words[index].Hide();
            }
        }

        public bool AllWordsHidden()
        {
            return _words.All(w => w.IsHidden());
        }
    }
}
