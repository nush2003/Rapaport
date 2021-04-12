using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapaportTest.Pages
{
    public class UploadSingleDiamondPage
    {
        static TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");


        public UploadSingleDiamondPage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        private IWebDriver Driver { get; }

        IWebElement uploadSingleDiamondLink => Driver.FindElement(By.XPath("/html/body/div[1]/div/main/section[2]/nav/a[1]"));
        IWebElement stockField => Driver.FindElement(By.Id("vendorStocknumber"));
        IWebElement askingPriceField => Driver.FindElement(By.Id("listDiscount"));
        IWebElement askingPriceRapField => Driver.FindElement(By.XPath("/html/body/div[1]/div/main/section[2]/div/section/div/form/div/div[2]/div/div/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/div/div/label"));
        IWebElement selectShapeField => Driver.FindElement(By.XPath("/html/body/div[1]/div/main/section[2]/div/section/div/form/div/div[2]/div/div/div[5]/div[1]/div/div[2]/div/div/div/div/div[2]/div"));
        IWebElement sizeField => Driver.FindElement(By.Id("size"));
        IWebElement colorSelectField => Driver.FindElement(By.XPath("/html/body/div[1]/div/main/section[2]/div/section/div/form/div/div[2]/div/div/div[5]/div[3]/div/div[2]/div/div/div/div/div[2]/div"));
        IWebElement claritySelectField => Driver.FindElement(By.XPath("/html/body/div[1]/div/main/section[2]/div/section/div/form/div/div[2]/div/div/div[5]/div[4]/div/div[2]/div/div/div/div/div[2]/div"));
        IWebElement saveLotBtn => Driver.FindElement(By.XPath("/html/body/div[1]/div/main/section[2]/div/section/div/form/div/div[2]/div/div/div[12]/button[2]"));
        IWebElement uploadPopup => Driver.FindElement(By.XPath("//*[@id=\"body\"]/div[5]/div/div"));
        IWebElement uploadHistoryBtn => Driver.FindElement(By.XPath("//*[@id=\"body\"]/div[5]/div/div/div/section/div[2]/div/div[4]/a[1]"));


        public void clickOnUplSingleDiamondLink() => uploadSingleDiamondLink.Click();

        public string uploadNewDiamond(string stock, string askPrice, string rap, string shape, string size, string color, string clarity)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));

            //Fill in Compulsory fields for uploading a new diamond
            stockField.SendKeys(stock);
            if (rap.Equals("true"))
            {
                askingPriceRapField.Click();
            }
            askingPriceField.SendKeys(askPrice);

            selectShapeField.Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id=\"uploadSingleForm\"]/div/div[2]/div/div/div[5]/div[1]/div/div[2]/div/div/div/div[2]/div/div[contains(text(),'{shape}')]"))).Click();

            sizeField.SendKeys(size);

            colorSelectField.Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id=\"uploadSingleForm\"]/div/div[2]/div/div/div[5]/div[3]/div/div[2]/div/div/div/div[2]/div/div[contains(text(),'{color}')]"))).Click();

            claritySelectField.Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id=\"uploadSingleForm\"]/div/div[2]/div/div/div[5]/div[4]/div/div[2]/div/div/div/div[2]/div/div[contains(text(),'{clarity}')]"))).Click();

            saveLotBtn.Click();
            string currentEasternTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo).ToString("hh:mm tt");

            //verify 'Upload processed popup' is displayed
            Assert.That(uploadPopup.Displayed, Is.True);
            uploadHistoryBtn.Click();

            return currentEasternTime;
        }

       

    }
}
