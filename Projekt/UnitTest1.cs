using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Threading;
using FlaUI.Core.Definitions;

namespace FlaUiTests
{
    [TestClass]
    public class OBS_Tests
    {
        FlaUI.Core.Application application;
        private readonly string appPath = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\OBS Studio\OBS Studio (64bit)";
        private const string APP_TITLE = "OBS 27.1.1 (64-bit, windows) - Profile: 1tytułu - Sceny: 2";
        private const string APP_TITLE2 = "OBS Studio 27.1.1 (64-bit, windows) - Profile: 1tytułu - Sceny: 2";


        private int sleepTimeShort = 1000;
        private int sleepTimeNormal = 2000;
        private int sleepTimeLong = 3000;

        [TestMethod]
        public void NagrywanieSceny()
        {
            application = FlaUI.Core.Application.Launch(appPath);
            Thread.Sleep(sleepTimeLong);
            var automation = new UIA3Automation();
            var mainWindow = application.GetMainWindow(automation);
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
            Assert.AreEqual(mainWindow.Name, APP_TITLE);
            Thread.Sleep(sleepTimeShort);

            var status = mainWindow.FindFirstDescendant(cf.ByAutomationId("OBSBasic.statusbar"));
            var name = status.FindFirstDescendant(cf.ByName("REC: 00:00:00"));
           

            mainWindow.FindFirstDescendant(cf.ByName("Rozpocznij nagrywanie")).AsButton().Click();
            Thread.Sleep(sleepTimeLong);
            Assert.AreNotEqual(name.Name, "REC: 00:00:00");

            mainWindow.FindFirstDescendant(cf.ByName("Pauzuj nagrywanie")).AsButton().Click();
            Thread.Sleep(sleepTimeLong);
            Assert.AreNotEqual(name.Name, "00:00:02 (PAUSED");

            status = mainWindow.FindFirstDescendant(cf.ByAutomationId("OBSBasic.statusbar"));
            var name2 = status.FindFirstDescendant(cf.ByName("REC: 00:00:02 (PAUSED)"));

            mainWindow.FindFirstDescendant(cf.ByName("Wznów nagrywanie")).AsButton().Click();
            Thread.Sleep(sleepTimeLong);
            
            Assert.AreNotEqual(name2.Name, "REC: 00:00:02 (PAUSED)");

            mainWindow.FindFirstDescendant(cf.ByName("Zatrzymaj nagrywanie")).AsButton().Click();
            Thread.Sleep(sleepTimeLong);
            Assert.AreEqual(name.Name, "REC: 00:00:00");

            Thread.Sleep(sleepTimeShort);

            application.Close();
        }
        [TestMethod]
        public void EfektPrzejscia()
        {
            var scena_1 = "Scena 1";
            var scena_2 = "Scena 2";
            application = FlaUI.Core.Application.Launch(appPath);
            Thread.Sleep(sleepTimeLong);
            var automation = new UIA3Automation();
            var mainWindow = application.GetMainWindow(automation);
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            Assert.AreEqual(mainWindow.Name, APP_TITLE);
            Thread.Sleep(sleepTimeShort);

            var studio = mainWindow.FindFirstDescendant(cf.ByName("Tryb studia"));
            studio.AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            var tekst = mainWindow.FindFirstDescendant(cf.ByAutomationId("OBSBasic.centralwidget.previewLabel"));
            Thread.Sleep(sleepTimeShort);

            Assert.AreEqual(tekst.AsLabel().Text, "Podgląd");
            var mainWindow2 = application.GetMainWindow(automation);
            ConditionFactory cf2 = new ConditionFactory(new UIA3PropertyLibrary());

            Assert.AreEqual(mainWindow2.Name, APP_TITLE2);
            Thread.Sleep(sleepTimeShort);

            var teskt2 = mainWindow.FindFirstDescendant(cf.ByName("Scena 1"));
            teskt2.AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            var podglad = mainWindow.FindFirstDescendant(cf.ByClassName("OBSBasicPreview"));
            podglad.AsButton().Click();
            var kolor = mainWindow.FindFirstDescendant(cf.ByName("#d1ca01"));
            Assert.AreEqual(kolor.AsLabel().Text, "#d1ca01");

            Thread.Sleep(sleepTimeShort);
            
            var plus = mainWindow.FindFirstDescendant(cf.ByAutomationId("OBSBasic.centralwidget"));
            var plus2 = plus.FindFirstByXPath("/Group[2]/Button[3]");
            plus2.Click();
            Thread.Sleep(sleepTimeShort);

            AutomationElement desktop = automation.GetDesktop();
            AutomationElement obs = desktop.FindFirstDescendant(f => f.ByName("obs64"));

            obs.FindFirstByXPath("/MenuItem[3]").AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            plus.FindFirstByXPath("/Group[2]/Button[4]").Click();
            Thread.Sleep(sleepTimeShort);

            podglad.AsButton().Click();
            var kolor2 = mainWindow.FindFirstDescendant(cf.ByName("#d10000"));
            Assert.AreEqual(kolor2.AsLabel().Text, "#d10000");

            studio.AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            Assert.AreEqual(mainWindow.Name, APP_TITLE);

            var teskt3 = mainWindow.FindFirstDescendant(cf.ByName("Scena"));
            teskt3.AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            application.Close();
        }
        [TestMethod]
        public void ZmianaMotywu()
        {
            var scena_1 = "Scena 1";
            var scena_2 = "Scena 2";
            application = FlaUI.Core.Application.Launch(appPath);
            Thread.Sleep(sleepTimeLong);
            var automation = new UIA3Automation();
            var mainWindow = application.GetMainWindow(automation);
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
            Assert.AreEqual(mainWindow.Name, APP_TITLE);
            Thread.Sleep(sleepTimeShort);

            var ustawienia = mainWindow.FindFirstDescendant(cf.ByName("Ustawienia"));
            ustawienia.AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            var tekst = mainWindow.FindFirstDescendant(cf.ByAutomationId("OBSBasic.OBSBasicSettings.settingsPages.generalPage.scrollArea_2.qt_scrollarea_viewport.scrollAreaWidgetContents_2.widget_2.groupBox_15.theme"));
            Thread.Sleep(sleepTimeShort);

            Assert.AreEqual(tekst.FindFirstByXPath("List/ListItem[2]").AsLabel().Text, "Dark (Domyślnie)");
            Thread.Sleep(sleepTimeShort);

            tekst.AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            var teskt2 = tekst.FindFirstByXPath("List/ListItem[3]");
            teskt2.AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            mainWindow.FindFirstDescendant(cf.ByName("Zastosuj")).AsButton().Click();
            Thread.Sleep(sleepTimeLong);

            mainWindow.FindFirstDescendant(cf.ByName("OK")).AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            ustawienia.AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            tekst = mainWindow.FindFirstDescendant(cf.ByAutomationId("OBSBasic.OBSBasicSettings.settingsPages.generalPage.scrollArea_2.qt_scrollarea_viewport.scrollAreaWidgetContents_2.widget_2.groupBox_15.theme"));
            Assert.AreEqual(tekst.FindFirstByXPath("List/ListItem[3]").AsLabel().Text, "Rachni");
            Thread.Sleep(sleepTimeShort);

            tekst.AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            teskt2 = tekst.FindFirstByXPath("List/ListItem[2]");
            teskt2.AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            mainWindow.FindFirstDescendant(cf.ByName("Zastosuj")).AsButton().Click();
            Thread.Sleep(sleepTimeLong);

            Assert.AreEqual(tekst.FindFirstByXPath("List/ListItem[2]").AsLabel().Text, "Dark (Domyślnie)");
            Thread.Sleep(sleepTimeShort);

            mainWindow.FindFirstDescendant(cf.ByName("OK")).AsButton().Click();
            Thread.Sleep(sleepTimeShort);

            application.Close();
        }
    }
}