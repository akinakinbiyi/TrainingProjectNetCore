using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace ChromeDriverTest
{
    [TestClass]
    public class ChromeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private ChromeDriver _driver;
        readonly string webAppUrl = "https://localhost:44379";

        [TestInitialize]
        public void ChromeDriverInitialize()
        {
            // Initialize Chrome driver 
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),chromeOptions);
            //{
                _driver.Manage().Window.Maximize();
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(300);
                _driver.Navigate().GoToUrl(webAppUrl);
            //    //Thread.Sleep(5000);
            //}
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
                    Assert.AreEqual("Home Page - TrainingProjectWeb", _driver.Title, "Expected title to be 'Home Page - TrainingProjectWeb'");
                    Thread.Sleep(5000);
        }


        [TestMethod]
        public void LoginTest()
        {
            while (true)
            {
                try
                {
                    _driver.FindElement(By.LinkText("Login")).Click();
                    Assert.AreEqual("Log in - TrainingProjectWeb", _driver.Title, "Expected title to be 'TrainingProjectWeb'");

                    _driver.FindElement(By.Name("Input.Email")).SendKeys("whoknows@mewstest.com");
                    _driver.FindElement(By.Name("Input.Password")).SendKeys("P@ssw0rd123!");
                    _driver.FindElement(By.Id("login-submit")).Click();

                    //Assert.AreEqual("Welcome", driver.FindElement(By.TagName("h1")).Text, "Epected title to be 'Welcome'");
                    break;
                }
                catch (Exception e)
                {
                    var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    //if (currentTimestamp > endTimstamp)
                    //{
                    //    Console.Write("##vso[task.logissue type=error;]Test SampleFunctionalTest1 failed with error: " + e.ToString());
                    //    throw;
                    //}
                    Thread.Sleep(50000000);
                }
            }
        }



        [TestCleanup]
        public void ChromeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}
