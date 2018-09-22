using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VowelHunt
{
    class Program
    {
        static void Main(string[] args)
        {
            var vowelChase = new VowelChaser();
            vowelChase.GetWord();
            vowelChase.Publish();
        }
    }

    class VowelChaser
    {
        string vowels = "aeiouAEIOU";
        string newWord;
        int counter;

        public void GetWord()
        {
            Console.Write("Please enter an English word: ");
            newWord = Console.ReadLine();

            VowelLocator(newWord);
        }

        public void VowelLocator(string input)
        {
            for(var i = 0; i < input.Length; i++)
            {
                if (vowels.Contains(input[i]))
                {
                    counter++;
                }
            }
        }

        public void Publish()
        {
            Console.WriteLine(counter);
        }
    }
}