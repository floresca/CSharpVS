using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamemaN
{
    class Program
    {
        static void Main(string[] args)
        {
            var NameSwitch = new GetName();
            NameSwitch.Namer();
            NameSwitch.Switcher();
            NameSwitch.Converter();
            NameSwitch.Publisher();
        }
    }

    class GetName
    {
        string oldName;
        public List<string> nameList = new List<string>();
        string newName;

        public void Namer()
        {
            Console.Write("Enter a name: ");
            oldName = Console.ReadLine();
        }

        public void Switcher()
        {
            for (int i = oldName.Length; i > 0; i--)
            {
                nameList.Add(oldName[i - 1].ToString());
            }
        }

        public void Converter()
        {
            StringBuilder build = new StringBuilder();
            foreach (string letter in nameList)
            {
                build.Append(letter).Append("");
            }
            newName = build.ToString();
        }

        public void Publisher()

        {
            Console.WriteLine(newName);
        }
    }
}
