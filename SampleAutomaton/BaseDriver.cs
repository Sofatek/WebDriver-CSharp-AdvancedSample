using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SampleAutomaton
{
    class BaseDriver
    {
        protected DriverManager dm = DriverManager.Instance;

        protected BaseDriver(IWebDriver driver = null)
        {
        }
        
        protected void GoToUrl(string urlCartebtp)
        {            
            IWebDriver driver = dm.GetDriver();

            driver.Navigate().GoToUrl(urlCartebtp);
        }

        protected void Refresh()
        {            
            IWebDriver driver = dm.GetDriver();

            driver.Navigate().Refresh();
        }

        protected void Back()
        {            
            IWebDriver driver = dm.GetDriver();

            driver.Navigate().Back();
        }

        protected void SwitchToFrame(int n)
        {            
            IWebDriver driver = dm.GetDriver();

            driver.SwitchTo().Frame(n);
        }
        protected void SwitchToParentFrame()
        {            
            IWebDriver driver = dm.GetDriver();

            driver.SwitchTo().ParentFrame();
        }

        protected IAlert SwitchToAlert()
        {            
            IWebDriver driver = dm.GetDriver();

            return driver.SwitchTo().Alert();
        }

        public static void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        protected IWebElement WaitFor(string selector)
        {
            By by = GetBy(selector);

            return WaitFor(by);
        }

        public IWebElement WaitFor(By by)
        {            
            IWebDriver driver = dm.GetDriver();

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, config.explicitWaitTimeOut));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));

            return element;
        }

        // Present and displayed
        protected bool IsElementPresent(string selector)
        {
            By by = GetBy(selector);

            return IsElementPresent(by);
        }

        // Present and displayed
        private bool IsElementPresent(By by)
        { 
            IWebElement element;
            
            IWebDriver driver = dm.GetDriver();

            try
            {
                // Turn off implicitwait
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                element = driver.FindElement(by);
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            finally
            {
                // Turn on implicit wait
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(config.defaultWaitTimeOut);
            }
        }

        protected bool IsAlertPresent()
        {            
            IWebDriver driver = dm.GetDriver();

            try
            {
                var switchTo = driver.SwitchTo();
                Wait(1500);
                switchTo.Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
            catch (UnhandledAlertException e)
            {
                Console.WriteLine(e);
                //Find("id=alert").Click();
            }

            return false;
        }

        /// <summary>
        /// Find webdriver element based on selector. If selector starts with // then by xpath, if selectors starts with id= then by id else by css
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        protected IWebElement Find(string selector)
        {
            By by = GetBy(selector);
            
            IWebDriver driver = dm.GetDriver();

            return driver.FindElement(by);
        }

        private By GetBy(string selectorText)
        {
            By by = null;
            if (selectorText.StartsWith("//"))
            {
                by = By.XPath(selectorText);
            }
            else if (selectorText.StartsWith("xpath="))
            {
                by = By.XPath(selectorText.Substring(6));
            }
            else if (selectorText.StartsWith("id="))
            {
                by = By.Id(selectorText.Substring(3));
            }
            else if (selectorText.StartsWith("name="))
            {
                string name = selectorText.Substring(5);
                by = By.XPath("//*[@name='" + name + "']");
            }
            else if (selectorText.StartsWith("linkText="))
            {
                string linkText = selectorText.Substring(9);
                by = By.XPath("//a[text()='"+linkText+"']");
            }
            else
            {
                by = By.CssSelector(selectorText);
            }

            return by;
        }

        protected ReadOnlyCollection<IWebElement> FindElts(string selector)
        {
            By by = GetBy(selector);
            
            IWebDriver driver = dm.GetDriver();

            return driver.FindElements(by);
        }
    }
}
