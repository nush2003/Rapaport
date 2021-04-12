using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RapaportTest.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

namespace RapaportTest.Tests
{
    class mainTest
    {
        static string urlBase = "https://trade.stage.rapnet.com/#";
        string currentEasternTime = null;
        string currentDate = DateTime.UtcNow.ToString("MM.dd.yyyy");


        //Browser driver
        static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        static IWebDriver webDriver = new ChromeDriver("" + projectDirectory + "\\driver");

        LoginPage loginPage = new LoginPage(webDriver);
        HomePage homePage = new HomePage(webDriver);
        DiamondMenuPage dmMenuPage = new DiamondMenuPage(webDriver);
        UploadSingleDiamondPage uplSingleDiamonPage = new UploadSingleDiamondPage(webDriver);
        UploadHistoryPage uplHistoryPage = new UploadHistoryPage(webDriver);


        [SetUp]
        public void Setup()
        {
            //Navigate to site
            webDriver.Manage().Window.Maximize();           
            webDriver.Navigate().GoToUrl($"{urlBase}/login");
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [Test]
        public void diamondTest()
        {
            //load input xml file with all parameters
            XmlDocument doc = new XmlDocument();
            doc.Load(projectDirectory + "\\inputData.xml");
            XmlElement root = doc.DocumentElement;

            //load expected results file with all parameters
            XmlDocument doc2 = new XmlDocument();
            doc.Load(projectDirectory + "\\expectedResults.xml");
            XmlElement root2 = doc.DocumentElement;


            //login to https://trade.stage.rapnet.com/#/login page with given credentials
            loginPage.Login(root.SelectSingleNode("loginPage/username").InnerText, root.SelectSingleNode("loginPage/password").InnerText);

            //verify welcome message contains the logged in user's details
            Assert.That(homePage.isWelcomeMessageDisplayedProperly("Eric", "Rapaport QA Test"), Is.True);

            //go to upload single diamond page
            dmMenuPage.clickOnDiamonIcon();
            dmMenuPage.clickOnUploadDiamondsLink();

            //verify url changed to be https://trade.stage.rapnet.com/#/upload
            Assert.That(webDriver.Url.Equals($"{urlBase}/upload"), Is.True);

            //crete single diamond with compulsory fields
            uplSingleDiamonPage.clickOnUplSingleDiamondLink();
            currentEasternTime =uplSingleDiamonPage.uploadNewDiamond(root.SelectSingleNode("uploadSingleDiamond/stock").InnerText,
                                                                     root.SelectSingleNode("uploadSingleDiamond/askingPrice").InnerText,
                                                                     root.SelectSingleNode("uploadSingleDiamond/rap").InnerText,
                                                                     root.SelectSingleNode("uploadSingleDiamond/shape").InnerText,
                                                                     root.SelectSingleNode("uploadSingleDiamond/size").InnerText,
                                                                     root.SelectSingleNode("uploadSingleDiamond/color").InnerText,
                                                                     root.SelectSingleNode("uploadSingleDiamond/clarity").InnerText);

            //verify url changed to be https://trade.stage.rapnet.com/#/uploadhistory
            Assert.That(webDriver.Url.Equals($"{urlBase}/uploadhistory"), Is.True);

            //check the data to be as expected
            uplHistoryPage.checkStatusOfUploadedDiamond(currentDate,currentEasternTime,
                                                        root2.SelectSingleNode("uploadedDiamond/status").InnerText,
                                                        root2.SelectSingleNode("uploadedDiamond/lotsReceived").InnerText,
                                                        root2.SelectSingleNode("uploadedDiamond/validLots").InnerText,
                                                        root2.SelectSingleNode("uploadedDiamond/invalidLots").InnerText,
                                                        root2.SelectSingleNode("uploadedDiamond/added").InnerText);
        }
         
        [TearDown]
        public void TearDown() => webDriver.Quit();
    }
}
