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
    /// <summary>
    /// Gestionnaire de navigateurs (Singleton)
    /// Gestion par pool
    /// </summary>
    public sealed class DriverManager
    {
	private IWebDriver driver = null;    

        private static readonly DriverManager _instance = new DriverManager();

        public static DriverManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public void GetDriver()
        {
           if (driver == null) {
              string driverCodeName = "CH";  // Chrome
            
              df = new DriverFactory();
              driver = df.GetDriver(driverCodeName);
           }
           
           return driver;
        }

        public void QuitAndDispose()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
