using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace Selenium
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\webdrivers";
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory);
            // _driver = new FirefoxDriver(DriverDirectory);  
            //_driver = new EdgeDriver(DriverDirectory);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            //_driver.Dispose();
        }

        [TestMethod]
        public void TestMethod1()
        {
            _driver.Navigate().GoToUrl("file:///C:/Users/45505/OneDrive%20-%20Zealand/3sem/Vscode/ProjectUI/Index.html");

            // Wait for the page to load
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            // Find the select element
            IWebElement selectElement = wait.Until(driver =>
                driver.FindElement(By.Id("SelectGraph")));

            // Create a SelectElement to interact with the dropdown
            var dropdown = new SelectElement(selectElement);

            Assert.AreEqual(4, dropdown.Options.Count, "Dropdown should have 3 options");

            // Verify the initial selected option (if applicable)
            string initialSelectedOption = dropdown.SelectedOption.Text;
            Assert.IsNotNull(initialSelectedOption, "Initial dropdown option should not be null");

            // Select an option
            dropdown.SelectByText("Sidste 50 målinger");

            // Verify the selected option changed
            Assert.AreNotEqual(initialSelectedOption, dropdown.SelectedOption.Text,
                "Selected option should be different from initial option");

            // Wait for the chart to update
            IWebElement chartCanvas = wait.Until(d => d.FindElement(By.Id("co2Chart")));
            Assert.IsNotNull(chartCanvas, "Chart canvas should be present");

            // Check chart visibility
            Assert.IsTrue(chartCanvas.Displayed, "Chart should be visible");
    //        var jsExecutor = (IJavaScriptExecutor)_driver;

    //        var dataPointsCount = jsExecutor.ExecuteScript(@"
    //    // More comprehensive check for Vue instance
    //    var app = document.querySelector('#app');
    //    if (app && app.__vue__) {
    //        return app.__vue__.graphData ? app.__vue__.graphData.length : 'graphData not found';
    //    }
    //    return 'Vue instance not found';
    //");

    //        // Assert that the number of data points is 20
    //        Assert.AreEqual(50, Convert.ToInt32(dataPointsCount), "Chart should contain 50 data points");
        }
    }
}