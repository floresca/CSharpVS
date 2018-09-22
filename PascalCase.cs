using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalCase
{
    class Program
    {
        static void Main(string[] args)
        {
            var stuff = new Sentence();
            stuff.GetWords();
            stuff.Capitalize();
            stuff.JoinAndPublish();
        }
    }

    class Sentence
    {
        public List<string> lowerCaseList = new List<string>();
        string sentence;

        public void GetWords()
        {
            Console.Write("Enter a sentence: ");
            sentence = Console.ReadLine();

            Split(sentence);
        }

        public void Split(string input)
        {
            string[] wordList = input.Split(' ');

            foreach (string word in wordList)
            {
                lowerCaseList.Add(word.ToLower());
            }
        }

        public void Capitalize()
        {
            for (var i = 0; i < lowerCaseList.Count; i++)
            {
                var letter = char.ToUpper(lowerCaseList[i][0]);
                lowerCaseList[i] = letter.ToString() + lowerCaseList[i].Substring(1);
            }
        }

        public void JoinAndPublish()
        {
            var publishing = string.Join("", lowerCaseList);
            Console.WriteLine(publishing);
        }
    }
}