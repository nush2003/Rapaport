using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapaportTest.Pages
{
    public class UploadHistoryPage
    {
        public UploadHistoryPage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        private IWebDriver Driver { get; }

        IList <IWebElement> uploadHistoryList => Driver.FindElements(By.XPath("//*[@id=\"root\"]/div/main/section[2]/div/section/div/div"));
        IWebElement uplHisDate => Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/section[2]/div/section/div/div[1]/div/div[1]/div/div/div[1]"));
        IWebElement uplHisTime => Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/section[2]/div/section/div/div[1]/div/div[1]/div/div/div[2]"));
        IWebElement uplHisStatus => Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/section[2]/div/section/div/div[1]/div/div[2]/div[2]/div[2]/div"));
        IWebElement uplHisLotsReceived => Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/section[2]/div/section/div/div[1]/div/div[2]/div[4]/div[2]/div"));
        IWebElement uplHisValidLots => Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/section[2]/div/section/div/div[1]/div/div[2]/div[5]/div[2]/div"));
        IWebElement uplHisInvalidLots => Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/section[2]/div/section/div/div[1]/div/div[2]/div[6]/div[2]/div"));
        IWebElement uplHisAdded => Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/main/section[2]/div/section/div/div[1]/div/div[2]/div[8]/div[2]/div"));


        public void checkStatusOfUploadedDiamond(string curDate, string curEasternTime, string status,string lotsReceived,string validLots,string invalidLots,string added)
        {
            Assert.That(uploadHistoryList.Count > 0, Is.True);

            Assert.That(uplHisDate.Text.Equals(curDate), Is.True);
            Assert.That(uplHisTime.Text.Equals(curEasternTime), Is.True);           
            Assert.That(uplHisStatus.Text.Equals(status), Is.True);          
            Assert.That(uplHisLotsReceived.Text.Equals(lotsReceived), Is.True);           
            Assert.That(uplHisValidLots.Text.Equals(validLots), Is.True);           
            Assert.That(uplHisInvalidLots.Text.Equals(invalidLots), Is.True);           
            Assert.That(uplHisAdded.Text.Equals(added), Is.True);
        }
    }
}
