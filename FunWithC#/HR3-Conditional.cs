using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution {

// If N is odd, print Weird
// If N is even and in the inclusive range of 2 to 5, print Not Weird
// If N is even and in the inclusive range of 6 to 20, print Weird
// If N is even and greater than 20, print Not Weird

    static void Main(string[] args) {
        int N = Convert.ToInt32(Console.ReadLine());

        if (N % 2 == 0 && (N >= 2 && N <= 5 || N > 20))
        {
            Console.WriteLine("Not Weird");
        }
        else
        {
            Console.WriteLine("Weird");
        }
    }
}
