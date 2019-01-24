using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            var hello = new Scraper();
            hello.Navigate();
        }
    }

    class Scraper
    {
        public void Navigate()
        {
            using (var potatoes = new ChromeDriver(Environment.CurrentDirectory))
            {
                potatoes.Url = "https://login.yahoo.com";
                potatoes.Navigate();

                //Console.Write("Please enter your user name: ");
                string userName = "floresc.andres@gmail.com";

                potatoes.FindElementById("login-username").SendKeys(userName);
                potatoes.FindElementById("login-signin").Click();

                //potatoes.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                WebDriverWait espera = new WebDriverWait(potatoes, TimeSpan.FromSeconds(30));
                IWebElement input = espera.Until(ExpectedConditions.ElementIsVisible(By.Id("login-passwd")));

                Console.Write("Enter your password: ");
                string password = "TMG72gyS";

                input.SendKeys(password);
                potatoes.FindElementById("login-signin").Click();

                potatoes.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");

                espera.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='pf-detail-table']/div[1]/table/tbody/tr[1]/td[1]")));
                //*[@id="pf-detail-table"]/div[1]/table/tbody/tr[1]/td[1]
                //*[@id="pf-detail-table"]/div[1]/table/tbody
                //*[@id="pf-detail-table"]/div[1]/table/tbody/tr[1]
                //*[@id="Lead-2-Portfolios-Proxy"]/main/div/div[2]

                int counter = 0;
                foreach (var piece in potatoes.FindElementsByXPath("//*[@id='pf-detail-table']/div[1]/table/tbody/tr["+counter+"]"))
                {
                    string[] cake = piece.Text.Split(' ');
                    counter++;

                    string ticker = cake[0];
                    string stockPrice = cake[1];
                    string stockPriceChange = cake[2];
                    string stockPricePercentChange = cake[3];
                    string exchangeVolume = cake[8];
                    string averageVolume3Months = cake[10];
                    string marketCap = cake[11];

                    Console.WriteLine("The stock price for {0} is {1} while their market cap is {2}", ticker, stockPrice, marketCap);
                }
            }
        }
    }
}





// using (var potatoes = new ChromeDriver(Environment.CurrentDirectory))
//     {
//         potatoes.Url = "https://login.yahoo.com";
//         potatoes.Navigate();

//         Console.Write("Please enter your user name: ");
//         string userName = Console.ReadLine();

//         potatoes.FindElementById("login-username").SendKeys(userName);
//         potatoes.FindElementById("login-signin").Click();

//         WebDriverWait espera = new WebDriverWait(potatoes, TimeSpan.FromSeconds(20));
//         IWebElement input = espera.Until(ExpectedConditions.ElementIsVisible(By.Id("login-passwd")));

//         Console.Write("Enter your password: ");
//         string password = Console.ReadLine();

//         input.SendKeys(password);
//         potatoes.FindElementById("login-signin").Click();

//         potatoes.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");

//         espera.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main']/section/section[2]/div[2]/table/tbody")));

//         foreach (var ticker in potatoes.FindElementsByXPath("//*[@id='main']/section/section[2]/div[2]/table"))
//         {
//             Console.WriteLine(ticker.Text);
            
//             entity framework SQL
//         }
//     }
    
// var urlGet = new HtmlWeb();
// var document = urlGet.Load("https://finance.yahoo.com/portfolio/p_0/view/v1");
// var liTags = document.DocumentNode.SelectNodes("//table[@class='_1TagL']");

// if (liTags != null)
// {
//     foreach (var li in liTags)
//     {
//         string tagcontent = li.InnerHtml;
//         Console.WriteLine(tagcontent);
//     }
// }


// create browser reference
// IWebDriver driver = new ChromeDriver();

// static void Main(string[] args)
// {

// }

// [Test]
// public void Initialize()
// {
//     //Navigate to Google page
//     driver.Navigate().GoToUrl("http://www.google.com");
// }

// public void ExecuteTest()
// {
//     //Find the input
//     IWebElement input = driver.FindElement(By.Name("q"));

//     //perform Ops
//     input.SendKeys("executeautomation");
// }

// public void CleanUp()
// {
//     driver.Close();
// }