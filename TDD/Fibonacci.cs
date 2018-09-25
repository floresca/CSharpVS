using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NUnit.Framework;

namespace TDD_Fibonacci
{
    /* Sequence of Fibonacci Numbers
     * 1,1,2,3,5,8,13,21,34
     * or
     * 0,1,1,2,3,5,8,13,21,34
     */

    [TestFixture]
    public class FibonacciTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void TestFibonacci(int expected, int index)
        {
            Assert.AreEqual(expected, GetFib(index));
        }

        private int GetFib(int index)
        {
            if (index == 0) return 0;
            if (index == 1) return 1;
            return GetFib(index - 1) + GetFib(index - 2);
        }
    }
}
