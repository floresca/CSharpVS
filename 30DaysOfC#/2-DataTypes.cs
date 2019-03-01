using System;
using System.Collections.Generic;
using System.IO;

class Solution {
    static void Main(String[] args) {
        int i = 4;
        double d = 4.0;
        string s = "HackerRank ";

        int num = Int32.Parse(Console.ReadLine());
        double dec = Convert.ToDouble(Console.ReadLine()); 
        string yo = Console.ReadLine();
                
        Console.WriteLine("{0}", num + i);
        Console.WriteLine("{0:F1}", dec + d);
        Console.WriteLine("{0}", s + yo);

    }
}