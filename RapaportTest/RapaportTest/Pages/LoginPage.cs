using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapaportTest.Pages
{
    public class LoginPage
    {
        public LoginPage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }
        private IWebDriver Driver { get; }

        public IWebElement txtUserName => Driver.FindElement(By.Id("emailUserName"));
        public IWebElement txtPassword => Driver.FindElement(By.Id("password"));
        public IWebElement btnLogin => Driver.FindElement(By.Id("btn-login"));


        public void Login(string userName,string password)
        {
            txtUserName.SendKeys(userName);
            txtPassword.SendKeys(password);
            btnLogin.Submit();
        }
    }
}
