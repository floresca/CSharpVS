using System;

namespace facebook
{
    class Program
    {
        static void Main(string[] args)
        {
            var sayIt = new Likes();
            sayIt.Naming();
            sayIt.Messages(sayIt.qtyOfPpl);
        }
    }

    public class People
    {
        public string[] people = new string[2] {"k", "j"};
        public int qtyOfPpl = 0;

        public void Naming()
        {
            while (true)
            {
                Console.Write("Enter a name: ");
                string newName = Console.ReadLine();

                if (newName == "")
                {
                    break;
                }
                else 
                {
                    if (qtyOfPpl < 2)
                    {
                        people[qtyOfPpl] = newName;
                        
                    }
                    qtyOfPpl++;
                }
            }
        }
    }

    public class Likes : People
    {
        public void Messages(int PPL)
        {
            if (PPL > 2)
            {
                Console.WriteLine(people[0] + ", " + people[1] + " and " + (PPL - 2) + " others have liked your post!");
            }
            else
            {
                switch (PPL)
                {
                    case 0:
                        Console.WriteLine("No likes yet, check back later");
                        break;
                    case 1:
                        Console.WriteLine(people[0] + " has liked your post!");
                        break;
                    case 2:
                        Console.WriteLine(people[0] + " and " + people[1] +" have liked your post!");
                        break;
                    case 3:
                        Console.WriteLine(people[0] + ", " + people[1] + " and " + (PPL - 2) + " other have liked your post!");
                        break;
                }
            }
        }
    }
}