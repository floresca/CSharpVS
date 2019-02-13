using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace YahooScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            var hello = new Scraper();
            hello.GoScrape();
        }
    }

    class Scraper
    {
        List<string[]> elementsArray = new List<string[]>();

        public void GoScrape()
        {
            using (var potatoes = new ChromeDriver(Environment.CurrentDirectory))
            {
                potatoes.Url = "https://login.yahoo.com";
                potatoes.Navigate();

                string userName = "world";

                potatoes.FindElementById("login-username").SendKeys(userName);
                potatoes.FindElementById("login-signin").Click();

                WebDriverWait espera = new WebDriverWait(potatoes, TimeSpan.FromSeconds(20));
                IWebElement input = espera.Until(ExpectedConditions.ElementIsVisible(By.Id("login-passwd")));

                string password = "hello";

                input.SendKeys(password);

                potatoes.FindElementById("login-signin").Click();

                potatoes.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");

                espera.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='pf-detail-table']/div[1]/table/tbody")));
                
                var tableBody = potatoes.FindElementByXPath("//*[@id='pf-detail-table']/div[1]/table/tbody");

                DataPull(tableBody);
            }
        }

        public void DataPull(IWebElement tableBody)
        {
            foreach (var tableData in tableBody.FindElements(By.TagName("tr")))
            {
                int counter = 0;
                string[] smallArray = new string[17];

                foreach (var tableDataPieces in tableData.FindElements(By.TagName("td")))
                {
                    smallArray[counter] = tableDataPieces.Text;
                    counter++;
                }

                elementsArray.Add(smallArray);
            }

            DataSave();
        }

        public void DataSave()
        {
            SqlConnection stocksDatabase = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\FloresPC\Desktop\Programming\Scraper\YahooScrape\YahooScrape\yahooStocksData.mdf; Integrated Security = True");

            stocksDatabase.Open();

            for (int i = 0; i < elementsArray.Count; i++)
            {
                SqlCommand items = new SqlCommand("INSERT INTO Stocks VALUES ( '" + elementsArray[i][0] + "', '" + elementsArray[i][1] + "', '" + elementsArray[i][2] + "', '" + elementsArray[i][3] + "', '" + elementsArray[i][6] + "', '" + elementsArray[i][12] + "')", stocksDatabase);
                
                items.ExecuteNonQuery();
            }

            stocksDatabase.Close();
        }
        
        //add code to update values if they already exist
    }
}
