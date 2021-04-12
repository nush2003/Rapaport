using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapaportTest.Pages
{
    public class HomePage
    {

        public HomePage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        private IWebDriver Driver { get; }

        IWebElement welcomeMessage => Driver.FindElement(By.Id("pageHeaderText"));
       
     
        public bool isWelcomeMessageDisplayedProperly(string firstName,string companyName)
        {
            if (welcomeMessage.Displayed)
            {
                string txt = welcomeMessage.Text;
                if (txt.Equals($"Welcome {firstName}, {companyName}"))
                {
                    return true;
                }
            }
            return false;
        }
           
       
    }
}
