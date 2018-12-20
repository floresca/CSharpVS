using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriScrape
{
    class Program
    {
        static void Main(string[] args)z
        {
            var nutrition = new Food();
            nutrition.UserInput();
        }
    }

    class Food
    {
        public string input;
        
        
        public void UserInput()
        {
            while(true)
            {
                Console.Write("Enter a food item: ");
                input = Console.ReadLine();

                if (input == "end")
                {
                    Environment.Exit(0);
                }
                else
                {
                    string site = "https://www.nutritionix.com/food/" + input;
                    var urlGet = new HtmlWeb();
                    var document = urlGet.Load(site);
                    var Calories = document.DocumentNode.SelectNodes("//div[@tabindex='0']");

                    //THIS IS STILL NOT WORKING RIGHT
                    //I must nt be understanding the type of data coming out of the DocumentNode since it clearly is not printable to console
                    //Must convert data out of DocumentNode to something WriteLine method can print, like text
                    //Tried innerText and innerHtml to no result
                    Console.WriteLine(Calories.TextContent);

                }

            }
        }
    }

}
