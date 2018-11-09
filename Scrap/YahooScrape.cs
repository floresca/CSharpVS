using (var potatoes = new ChromeDriver(Environment.CurrentDirectory))
    {
        potatoes.Url = "https://login.yahoo.com";
        potatoes.Navigate();

        Console.Write("Please enter your user name: ");
        string userName = Console.ReadLine();

        potatoes.FindElementById("login-username").SendKeys(userName);
        potatoes.FindElementById("login-signin").Click();

        WebDriverWait espera = new WebDriverWait(potatoes, TimeSpan.FromSeconds(20));
        IWebElement input = espera.Until(ExpectedConditions.ElementIsVisible(By.Id("login-passwd")));

        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        input.SendKeys(password);
        potatoes.FindElementById("login-signin").Click();

        potatoes.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");

        espera.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main']/section/section[2]/div[2]/table/tbody")));

        foreach (var ticker in potatoes.FindElementsByXPath("//*[@id='main']/section/section[2]/div[2]/table"))
        {
            Console.WriteLine(ticker.Text);
        }
    }