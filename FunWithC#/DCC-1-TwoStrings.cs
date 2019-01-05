using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC_1_TwoStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            var figureOut = new stringDifference();
            figureOut.Split("this", "bear", 4);
        }
    }

    class stringDifference
    {
        int sumAllSteps = 0;

        public void Split(string inputA, string inputB, int size)
        {
            char[] arrayInputA = inputA.ToCharArray();
            char[] arrayInputB = inputB.ToCharArray();

            CalcDifference(arrayInputA, arrayInputB, size);
        }

        public void CalcDifference(char[] arrayA, char[] arrayB, int size)
        {
            for (int i = 0; i < size; i++)
            {
                sumAllSteps += Math.Abs(arrayA[i] - arrayB[i]);
            }

            Console.WriteLine(sumAllSteps);
        }

    }
        
}
