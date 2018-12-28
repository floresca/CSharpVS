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
        static void Main(string[] args)
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
                    
                    HtmlWeb urlGet = new HtmlWeb();
                    var document = urlGet.Load("https://www.nutritionix.com/food/" + input);
                    Console.WriteLine(document);

                    //var Calories = document.DocumentNode.SelectSingleNode("//span[@itemprop='calories']");



                    
                    var Calories = document.DocumentNode.Descendants("span").Where(node => node.Attributes["itemprop"] != null && node.Attributes["itemprop"].Value == "calories") ;

                    Console.WriteLine(Calories.);
                }

            }
        }
    }

    //class Scrape : Food
    //{
    //    public void URLGet()
    //    {
    //        var site = "https://www.google.com/search?q=" + input + "calories";
    //        var urlGet = new HtmlWeb();
    //        var document = urlGet.Load(site);
    //        //var Calories = document.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("itemprop") && d.Attributes["itemprop"].Contains("calories"));
    //        //var Calories = document.DocumentNode.SelectNodes("//span[contains (@itemprop, 'calories')]");

    //        //Console.WriteLine(Calories);

    //        var Calories = document.DocumentNode.SelectNodes("//*[@id='kno - nf - nc']/table/tbody/tr[2]/td/span[2]");
    //        Console.WriteLine(Calories);

    //    }
    //}
}
