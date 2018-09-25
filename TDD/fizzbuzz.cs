using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TDD_FizzBuzz
{
    /*
     * If divisible by 3 -> return "Fizz"
     * if divisible by 5 -> return "Buzz"
     * if dividisble by 3 and 5 -> return "FizzBuzz"
     * Otherwise -> return ""
     */

    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestFizzBuzz()
        {
            Assert.AreEqual("fizz", IsDivisible(3));
            Assert.AreEqual("buzz", IsDivisible(5));
            Assert.AreEqual("fizz", IsDivisible(6));
            Assert.AreEqual("", IsDivisible(7));
            Assert.AreEqual("fizzbuzz", IsDivisible(15));
        }

        public string IsDivisible(int index)
        {
            if (index % 5 == 0 && index % 3 == 0) return "fizzbuzz";
            if (index % 3 == 0) return "fizz";
            if (index % 5 == 0) return "buzz";
            return "";
        }
    }
}
