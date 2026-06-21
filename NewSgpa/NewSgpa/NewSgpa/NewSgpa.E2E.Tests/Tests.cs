using DevExpress.EasyTest.Framework;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

// To run functional tests for ASP.NET Web Forms and ASP.NET Core Blazor XAF Applications,
// install browser drivers: https://www.selenium.dev/documentation/getting_started/installing_browser_drivers/.
//
// -For Google Chrome: download "chromedriver.exe" from https://chromedriver.chromium.org/downloads.
// -For Microsoft Edge: download "msedgedriver.exe" from https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/.
//
// Selenium requires a path to the downloaded driver. Add a folder with the driver to the system's PATH variable.
//
// Refer to the following article for more information: https://docs.devexpress.com/eXpressAppFramework/403852/

namespace NewSgpa.Module.E2E.Tests
{
    public class NewSgpaTests : IDisposable
    {
        const string BlazorAppName = "NewSgpaBlazor";
        const string WinAppName = "NewSgpaWin";
        const string AppDBName = "NewSgpa";
        EasyTestFixtureContext FixtureContext { get; } = new EasyTestFixtureContext();

        public NewSgpaTests()
        {
            FixtureContext.RegisterApplications(
                new BlazorApplicationOptions(BlazorAppName, string.Format(@"{0}\..\..\..\..\NewSgpa.Blazor.Server", Environment.CurrentDirectory)),
                new WinApplicationOptions(WinAppName, string.Format(@"{0}\..\..\..\..\NewSgpa.Win\bin\EasyTest\net10.0-windows\NewSgpa.Win.exe", Environment.CurrentDirectory))
            );
            FixtureContext.RegisterDatabases(new DatabaseOptions(AppDBName, "NewSgpaEasyTest", server: @"(localdb)\mssqllocaldb"));
        }
        public void Dispose()
        {
            FixtureContext.CloseRunningApplications();
        }
        [Theory]
        [InlineData(BlazorAppName)]
        public void TestBlazorApp(string applicationName)
        {
            FixtureContext.DropDB(AppDBName);
            var appContext = FixtureContext.CreateApplicationContext(applicationName);
            appContext.RunApplication();
            appContext.GetForm().FillForm(("User Name", "Admin"));
            appContext.GetAction("Log In").Execute();
            Assert.True(appContext.Navigate("My Details"));
            Assert.True(appContext.Navigate("Role"));
            Assert.True(appContext.Navigate("Users"));
            Assert.True(appContext.Navigate("Reports.Dashboards"));
            Assert.True(appContext.Navigate("Reports.Reports"));
            Assert.True(appContext.Navigate("Reports.Audit Event"));
        }
        [Theory]
        [InlineData(WinAppName)]
        public void TestWinApp(string applicationName)
        {
            FixtureContext.DropDB(AppDBName);
            var appContext = FixtureContext.CreateApplicationContext(applicationName);
            appContext.RunApplication();
            appContext.GetForm().FillForm(("User Name", "Admin"));
            appContext.GetAction("Log In").Execute();
            Assert.True(appContext.Navigate("My Details"));
            Assert.True(appContext.Navigate("Role"));
            Assert.True(appContext.Navigate("Users"));
            Assert.True(appContext.Navigate("Reports.Dashboards"));
            Assert.True(appContext.Navigate("Reports.Reports"));
            Assert.True(appContext.Navigate("Reports.Audit Event"));
        }
    }
}
