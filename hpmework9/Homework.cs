using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace hpmework9
{
    [TestFixture]
    public class Homework
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            //Config browser window
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments
                (
                    //"--headless",
                    //"--start-fullscreen",
                    "--start-maximized",
                    "--disable-infobars"
                );

            //Creating the driver
            TestContext.WriteLine("Creating driver");
            driver = new ChromeDriver(chromeOptions);

        }

        [TearDown]
        public void TearDown()
        {
            TestContext.WriteLine("Kill driver");
            driver.Quit();
        }

        [Test]
        [Author("Vasyl Tymchyshyn")]
        public void RegisterUser()
        {
            //Create user
            TestContext.WriteLine("Create user");
            User user = new User().FillIn();

            //Locators
            By registerLink = By.Id("registerLink");
            By email = By.Id("Email");
            By name = By.Id("Name");
            By surname = By.Id("Surname");
            By company = By.Id("Company");
            By password = By.Id("Password");
            By confirmPassword = By.Id("ConfirmPassword");
            By registerButton = By.XPath("//input[@type='submit']");
            By helloEmail = By.XPath($"//a[contains(text(),'{user.Email}')]");
            By logOffLink = By.LinkText("Log off");
            By loginLink = By.Id("loginLink");
            By logInButton = By.XPath("//input[@value='Log in']");
            By userName = By.Id("userName");
            By userSurname = By.Id("userSurname");
            By userCompany = By.Id("UserCompany");
            By userEmail = By.Id("userEmail");

            //1. Navigate to https://homeworkdecoration20181213051012.azurewebsites.net/
            TestContext.WriteLine("Go to site");
            driver.Navigate().GoToUrl("https://homeworkdecoration20181213051012.azurewebsites.net/");

            //2. Click register
            TestContext.WriteLine("Click register");
            driver.FindElement(registerLink).Click();

            //3. Fill all fields
            TestContext.WriteLine("Fill all fields");
            driver.FindElement(email).SendKeys(user.Email);
            driver.FindElement(name).SendKeys(user.Name);
            driver.FindElement(surname).SendKeys(user.Surname);
            driver.FindElement(company).SendKeys(user.Company);
            driver.FindElement(password).SendKeys(user.Password);
            driver.FindElement(confirmPassword).SendKeys(user.Password);
            driver.FindElement(registerButton).Submit();

            //4.Click "Hello {Email}"
            TestContext.WriteLine("Click Hello {Email}");
            driver.FindElement(helloEmail).Click();

            //5. Verify the user data
            TestContext.WriteLine("Verify the user data");
            driver.FindElement(userName).Text.Should().Be(user.Name);
            driver.FindElement(userSurname).Text.Should().Be(user.Surname);
            driver.FindElement(userCompany).Text.Should().Be(user.Company);
            driver.FindElement(userEmail).Text.Should().Be(user.Email);

            //6. Click log off
            TestContext.WriteLine("Click log off");
            driver.FindElement(logOffLink).Click();

            //7. Click log in
            TestContext.WriteLine("Click log in");
            driver.FindElement(loginLink).Click();

            //8. Enter user login and password
            TestContext.WriteLine("Enter user login and password");
            driver.FindElement(email).SendKeys(user.Email);
            driver.FindElement(password).SendKeys(user.Password);
            driver.FindElement(logInButton).Click();

            //9. Verify that user is logged in correctly
            TestContext.WriteLine("Verify that user is logged in correctly");
            driver.FindElement(helloEmail).Text.Should().Contain(user.Email);

        }

    }
}
