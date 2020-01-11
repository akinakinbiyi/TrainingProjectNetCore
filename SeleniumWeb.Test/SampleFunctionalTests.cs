using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace SeleniumWeb.Test
{
    [TestClass]
    public class SampleFunctionalTests : Hooks
    {
        public SampleFunctionalTests() : base(BrowserType.Chrome)
        {

        }

        //private static TestContext testContext;
        //private RemoteWebDriver driver;
        readonly string webAppUrl = "https://localhost:44379";



        //[OneTimeSetUp]
        //public static void Initialize(TestContext testContext)
        //{
        //    SampleFunctionalTests.testContext = testContext;
        //}

        [TestInitialize]
        public void TestInit()
        {
            //driver = GetChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(300);
            driver.Navigate().GoToUrl(webAppUrl);
            Thread.Sleep(5000);

        }

        //[TestCleanup]
        //public void TestClean()
        //{
        //    driver.Quit();
        //}

        [TestMethod]
        public void TitleValidation()
        {
            //var webAppUrl = testContext.Properties["webAppUrl"].ToString();

            var startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var endTimstamp = startTimestamp + 60 * 10;
            while (true)
            {
                try
                {
                    driver.Navigate().GoToUrl(webAppUrl);
                    Assert.AreEqual("Home Page - TrainingProjectWeb", driver.Title, "Expected title to be 'Home Page - TrainingProjectWeb'");
                    break;
                }
                catch (Exception e)
                {
                    var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    if (currentTimestamp > endTimstamp)
                    {
                        Console.Write("##vso[task.logissue type=error;]Test SampleFunctionalTest1 failed with error: " + e.ToString());
                        throw;
                    }
                    Thread.Sleep(50000000);
                }
            }
        }

        [TestMethod]
        public void LoginTest()
        {
            while (true)
            {
                try
                {
                    driver.FindElement(By.LinkText("Login")).Click();
                    Assert.AreEqual("Log in - TrainingProjectWeb", driver.Title, "Expected title to be 'TrainingProjectWeb'");

                    driver.FindElement(By.Name("Input.Email")).SendKeys("whoknows@mewstest.com");
                    driver.FindElement(By.Name("Input.Password")).SendKeys("P@ssw0rd123!");
                    driver.FindElement(By.Id("login-submit")).Click();

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

        private RemoteWebDriver GetChromeDriver()
        {
            var path = Environment.GetEnvironmentVariable("ChromeWebDriver");
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox");

            if (!string.IsNullOrWhiteSpace(path))
            {
                return new ChromeDriver(path, options, TimeSpan.FromSeconds(300));
            }
            else
            {
                return new ChromeDriver(options);
            }
        }
    }
}