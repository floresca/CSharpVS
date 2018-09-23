using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var newFile = new Files();
            newFile.GrabText();
            newFile.SplitText();
        }
    }

    class Files
    {
        string textInput;
        int counter;
        private string wordBank;

        public void GrabText()
        {
            textInput = File.ReadAllText(@"C:\Users\Platanosalpastor\Desktop\intracitygeeks\VS CS\LoremIpsum.txt");
        }

        public void SplitText()
        {
            string [] wordList = textInput.Split(' ');

            WordCount(wordList);
            LongestWord(wordList);
        }

        public void WordCount(string[] input)
        {
            for(var i = 0; i < input.Length; i++)
            {
                counter++;
            }

            Console.WriteLine("There are {0} words in this file", counter);
        }

        public void LongestWord(string[] input)
        {
            wordBank = input[0];

            for(var i = 1; i < input.Length; i++)
            {
                if(input[i].Length > wordBank.Length)
                {
                    wordBank = input[i];
                }
            }

            Console.WriteLine("The longest word is {0}", wordBank);
        }
    }
}