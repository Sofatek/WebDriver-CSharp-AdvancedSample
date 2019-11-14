using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SampleAutomaton
{
    class Automaton : BaseDriver
    {
        public void Run()
        {
            // Navigate to url
            string url = "https://wapractice.herokuapp.com/sample1";
            NavigateToUrl(url);

	    // Fill the form and Submit            
            Find("input[name=username]").SendKeys("test");
            Find("input[name=password]").SendKeys("password");
            Find("form").Submit();

            Wait(1500);

            // Back to return to the form
            Back();

            Find("//input[@name='username']").SendKeys("test");
            Find("//input[@name='password']").SendKeys("pa$$word");
            Find("//input[@type='submit']").Click();
        }
    }
}
