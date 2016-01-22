using System;
using System.Collections.Generic;
using System.IO;

namespace Pronounceable_Password_Test.Fran
{
    public class WordGenerator
    {
        private static readonly Random Rand = new Random();
        private string[] _words;

        public WordGenerator(string wordsFilePath)
        {
            LoadWords(wordsFilePath);
        }

        public string GeneratePhrase(int numberOfWords)
        {
            return GeneratePhrase(numberOfWords, " ");
        }

        public string GeneratePhrase(int numberOfWords, string separator)
        {
            if (numberOfWords == 0)
                return null;

            if (numberOfWords == 1)
                return GetWord();

            return string.Join(separator, GetWords(numberOfWords));
        }

        private string GetWord()
        {
            var number = Rand.Next(0, _words.Length);
            return _words[number];
        }

        private IEnumerable<string> GetWords(int numberOfWords)
        {
            for (int i = 0; i < numberOfWords; i++)
            {
                yield return GetWord();
            }
        }

        private void LoadWords(string wordsFilePath)
        {
            if(!File.Exists(wordsFilePath))
                throw new InvalidOperationException($"Cannot file the words file at {wordsFilePath}");

            _words = File.ReadAllLines(wordsFilePath);
        }
    }
}