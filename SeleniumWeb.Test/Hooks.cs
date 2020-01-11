using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SeleniumWeb.Test
{
    public enum BrowserType
    {
        Chrome,
        Firefox
    }


    [TestClass]
    public class Hooks : Base
    {
        private readonly BrowserType _browserType;

        public Hooks(BrowserType browserType)
        {
            _browserType = browserType;
        }

        [TestInitialize]
        public void Initialize()
        {
            SelectWebDriver(_browserType);
        }


        public void SelectWebDriver(BrowserType browserType)
        {
            if (browserType == BrowserType.Chrome)
                driver = new ChromeDriver();
            else if (browserType == BrowserType.Firefox)
                driver = new FirefoxDriver();
        }


        //public void SelectWebDriver(BrowserType browserType)
        //{
        //    if (browserType == BrowserType.Chrome)
        //    {
        //        DesiredCapabilities desiredCapabilities = DesiredCapabilities.Chrome();
        //        desiredCapabilities.SetCapability("version", "");
        //        desiredCapabilities.SetCapability("platform", "LINUX");
        //        driver = new RemoteWebDriver(new Uri("hub"), desiredCapabilities);
        //    }

        //    else if (browserType == BrowserType.Firefox)
        //    {
        //        DesiredCapabilities desiredCapabilities = DesiredCapabilities.Firefox();
        //        desiredCapabilities.SetCapability("version", "");
        //        desiredCapabilities.SetCapability("platform", "LINUX");
        //        driver = new RemoteWebDriver(new Uri("hub"), desiredCapabilities);
        //    }
        //}

        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}
