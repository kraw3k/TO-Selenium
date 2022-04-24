using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Projekt_TO
{
    class Test
    {

        private IWebElement GetImage()
        {
            return driver.FindElement(By.XPath("//a[@href='/wiki/Plik:Pterodactylus_holotype_fly_mmartyniuk.png'] [@class='image']"));
        }

        private IWebElement GetDetailsButton()
        {
            return driver.FindElement(By.XPath("//a[contains(.,'Więcej szczegółów')]"));
        }

        private void GoBack()
        {
            driver.Navigate().Back();
        }

        private void Sleep()
        {
            Thread.Sleep(2000);
        }

        IWebDriver driver;
        Actions actionProvider;
        IJavaScriptExecutor js;

        [SetUp]
        public void Initialize()
        {
            var config = new EdgeConfig();
            //var config = new ChromeConfig();

            // Setup driver using DriverManager instead of manually instaling drivers.
            new DriverManager().SetUpDriver(config);

            driver = new EdgeDriver();
            //driver = new ChromeDriver();

            actionProvider = new Actions(driver);
            js = (IJavaScriptExecutor)driver;
        }

        [Test]
        public void OpenAppTest()
        {
            driver.Url = "https://pl.wikipedia.org/";
            Sleep();
        }

        [Test]
        public void CheckTabTitle()
        {
            Assert.IsTrue(driver.Title.Contains("Wikipedia"));
        }

        [Test]
        public void FillSearchInput()
        {
            IWebElement searchInput = driver.FindElement(By.ClassName("vector-search-box-input"));
            searchInput.SendKeys("Pterodaktyl");

            string inputValue = searchInput.GetAttribute("value");
            Assert.AreEqual("Pterodaktyl", inputValue);
        }

        [Test]
        public void GoToSelectedArticle()
        {
            IWebElement searchButton = driver.FindElement(By.Id("searchButton"));
            searchButton.Click();
            Sleep();
            Assert.AreEqual(driver.Url, "https://pl.wikipedia.org/wiki/Pterodaktyl");
        }

        [Test]
        public void CheckIfImageExist()
        {
            IWebElement image = GetImage();
            Assert.IsNotNull(image);
        }

        [Test]
        public void OpenImage()
        {
            IWebElement image = GetImage();
            image.Click();
            Sleep();
            Assert.IsTrue(driver.Title.Equals("Pterodactylus holotype fly mmartyniuk - Pterodaktyl – Wikipedia, wolna encyklopedia"));
        }

        [Test]
        public void CheckIfDetailsButtonExist()
        {
            IWebElement detailsButton = GetDetailsButton();
            Assert.IsNotNull(detailsButton);
        }

        [Test]
        public void ClickDetailsButton()
        {
            IWebElement detalisButton = GetDetailsButton();
            detalisButton.Click();
            Assert.AreEqual(driver.Url, "https://commons.wikimedia.org/wiki/File:Pterodactylus_holotype_fly_mmartyniuk.png");
            Assert.IsTrue(driver.Title.Contains("File:Pterodactylus"));
            Sleep();
            GoBack();
            Sleep();
        }

        [Test]
        public void CloseImage()
        {
            IWebElement closeButton = driver.FindElement(By.CssSelector(".mw-mmv-close"));
            Assert.IsNotNull(closeButton);
            closeButton.Click();
        }

        [Test]
        public void HoverLink()
        {
            IWebElement link = driver.FindElement(By.XPath("//a[contains(.,'pterozaura')]"));
            Assert.IsNotNull(link);

            
            actionProvider.MoveToElement(link).Perform();
            Sleep();
        }

        [Test]
        public void CheckIfPopupIsVisible()
        {
            IWebElement popup = driver.FindElement(By.XPath("//div[contains(@class,'mwe-popups')][contains(@style,'display: block')]"));
            Assert.IsTrue(popup.Displayed);
        }

        [Test]
        public void ClickImageInsidePopup()
        {
            IWebElement popupImage = driver.FindElement(By.XPath("//a[@class='mwe-popups-discreet']"));
            Assert.IsTrue(popupImage.Displayed);

            popupImage.Click();
            Sleep();
        }

        [Test]
        public void ExecuteJS()
        {
            try
            {
                js.ExecuteScript("alert('Test alert')");
            }
            catch
            {

            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

            Sleep();
            Assert.AreEqual("Test alert", alert.Text);
            alert.Accept();
            Sleep();
        }

        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
