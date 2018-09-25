using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TDD_RomanNumerals
{
    [TestFixture]
    public class Class1
    {
        [TestCase(1, "I")]
        [TestCase(5, "V")]
        [TestCase(10, "X")]
        [TestCase(2, "II")]
        [TestCase(4, "IV")]
        [TestCase(9, "IX")]
        public void TestRomanNumerals(int expected, string roman)
        {
            Assert.AreEqual(expected, Roman.Number(roman));
            
        }
    }

    public class Roman
    {
        private static Dictionary<char, int> map =
            new Dictionary<char, int>()
            {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000},

            };

        public static int Number(string index)
        {
            int result = 0;
            for (int i = 0; i < index.Length; i++)
            {
                if (i + 1 < index.Length && map[index[i]] < map[index[i + 1]])
                {
                    result -= map[index[i]];
                }
                else
                {
                    result += map[index[i]];
                }
            }
            return result;
        }
    }
}
