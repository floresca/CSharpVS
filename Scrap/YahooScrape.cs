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

            SqlCommand databaseAlterations = new SqlCommand();

            databaseAlterations.CommandText = "SELECT COUNT(id) FROM Stocks";
            databaseAlterations.Connection = stocksDatabase;
            Int32 count = (Int32)databaseAlterations.ExecuteScalar();

            int uniqueID = count;

            for (int i = 0; i < elementsArray.Count; i++)
            {
                uniqueID++;
                string ticker = elementsArray[i][0];
                string stockPrice = elementsArray[i][1];
                string priceChange = elementsArray[i][2];
                string pricePercentChange = elementsArray[i][3];
                string tradeVolume = elementsArray[i][6];
                string marketCap = elementsArray[i][12];


                databaseAlterations.CommandText = "INSERT INTO Stocks VALUES ( '" + uniqueID + "', '" + ticker + "', '" + stockPrice + "', '" + priceChange + "', '" + pricePercentChange + "', '" + tradeVolume + "', '" + marketCap + "', GETDATE())";
                databaseAlterations.Connection = stocksDatabase;
                databaseAlterations.ExecuteNonQuery();
            }

            stocksDatabase.Close();
        }
    }
}
