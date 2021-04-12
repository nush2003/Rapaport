using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapaportTest.Pages
{
    public class DiamondMenuPage
    {
        public DiamondMenuPage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        private IWebDriver Driver { get; }

        IWebElement diamondIcon => Driver.FindElement(By.Name("menuItemDiamond"));
        IWebElement uploadDiamondsLink => Driver.FindElement(By.XPath("/html/body/div[1]/div/main/section[1]/nav/ul/li[5]/a"));

        public void clickOnDiamonIcon() => diamondIcon.Click();
        public void clickOnUploadDiamondsLink() => uploadDiamondsLink.Click();
        

    }
}
