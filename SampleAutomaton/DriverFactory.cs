using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System.Threading;
using System.Security;

namespace SampleAutomaton
{
    public class DriverFactory
    {
        public DriverFactory()
        {
        }

        public static bool CheckDriverCodeName(string codeName)
        {
            switch (name)
            {
                case "IE":
                case "FF":
                case "CH":
                case "CHless":
                    return true;
                default:
                    return false;
            }
        }

        public IWebDriver GetDriver(string driverCodeName)
        {            
            IWebDriver driver = GetDriverByCodeName(driverCodeName);
            return driver;
        }

        private IWebDriver GetDriverByCodeName(string driverCodeName)
        {
            IWebDriver driver = null;
            if (CheckDriverCodeName(driverCodeName))
            {
                switch (driverCodeName)
                {
                    case "IE":
                        driver = GetInternetExplorer();
                        break;
                    case "FF":
                        driver = GetFirefox();
                        break;
                    case "CH":
                        driver = GetChrome();
                        break;
                    case "CHless":
                        // Headless browser
                        driver = GetChromeless();
                        break;
                    default:
                        Console.WriteLine("Unknow browser code name");
                        break;
                }

                // Maximize windows
                driver.Manage().Window.Maximize();
            }

            return driver;
        }

        private IWebDriver GetInternetExplorer()
        {            
            return new InternetExplorerDriver();
        }

        private IWebDriver GetFirefox()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.SetPreference("network.proxy.type", 1);
            options.SetPreference("network.proxy.http", "http://165.225.76.40");
            options.SetPreference("network.proxy.http_port", 80);

            return new FirefoxDriver(options);
        }

        private IWebDriver GetChrome()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", AppliConfig.Instance.tempPath);
            options.AddUserProfilePreference("profile.default_content_settings.popups", "0");
            options.AddUserProfilePreference("disable-popup-blocking", "true");

            IWebDriver driver = new ChromeDriver(options);
            return driver;
        }

        private IWebDriver GetChromeless()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");

            return new ChromeDriver(options);
        }
    }
}
