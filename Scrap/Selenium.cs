// using NUnit.Framework;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrape
{
    class Program
    {

        IWebDriver driver = new ChromeDriver();

        static void Main(string[] args)
        {
            
        }


        [Test]
        public void ExecuteTest()
        {
            IWebElement element = driver.FindElement(By.Name("q"));

            element.SendKeys("Executeautomation");
        }


    }
}
