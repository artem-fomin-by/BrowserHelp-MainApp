using NUnit.Framework;

using MainApp;
using Logic;
using Logic.BrowserLogic;

namespace MainProgramTests{
    public class Tests{
        private static string OrigHTTPDefaultBrowserProgID;
        private static string OrigHTTPSDefaultBrowserProgID;

        [SetUp]
        public void Setup(){
            OrigHTTPDefaultBrowserProgID = (string) WorkWithReg
                .GetKey(BrowserServ.HTTPDefaultBrowserKeyPath)
                .GetValue("ProgID");
            OrigHTTPSDefaultBrowserProgID = (string) WorkWithReg
                .GetKey(BrowserServ.HTTPSDefaultBrowserKeyPath)
                .GetValue("ProgID");
        }
        
        [Test]
        public void InstallTest(){
            Program.Main(new[]{Program.STD_InstallCommand} );
            
            var appKey = WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath).OpenSubKey(Program.AppName);
            var progIDKey = WorkWithReg.GetKey(WorkWithReg.ProgIDsPath).OpenSubKey(Program.AppProgID);

            Assert.AreNotEqual(null, appKey);
            Assert.AreNotEqual(null, progIDKey);

            Assert.AreEqual(
                OrigHTTPDefaultBrowserProgID, 
                appKey.GetValue(Program.OrigHTTPProgIDValueName)
                );
            Assert.AreEqual(
                OrigHTTPSDefaultBrowserProgID, 
                appKey.GetValue(Program.OrigHTTPSProgIDValueName)
                );

            Assert.AreEqual(
                OrigHTTPDefaultBrowserProgID, 
                progIDKey.GetValue(Program.OrigHTTPProgIDValueName)
                );
            Assert.AreEqual(
                OrigHTTPSDefaultBrowserProgID, 
                progIDKey.GetValue(Program.OrigHTTPSProgIDValueName)
                );

            Assert.AreEqual(
                Program.AppProgID,
                WorkWithReg.GetKey(BrowserServ.HTTPDefaultBrowserKeyPath).GetValue("ProgID")
                );
            Assert.AreEqual(
                Program.AppProgID,
                WorkWithReg.GetKey(BrowserServ.HTTPSDefaultBrowserKeyPath).GetValue("ProgID")
            );
        }

        private void DeleteTest(string HTTPDefaultBrowserProgID, string HTTPSDefaultBrowserProgID){
            Assert.AreEqual(
                HTTPDefaultBrowserProgID, 
                WorkWithReg.GetKey(BrowserServ.HTTPDefaultBrowserKeyPath).GetValue("ProgID")
                );
            Assert.AreEqual(
                HTTPSDefaultBrowserProgID, 
                WorkWithReg.GetKey(BrowserServ.HTTPSDefaultBrowserKeyPath).GetValue("ProgID"));
        }

        [Test]
        public void DeleteTest1(){
            Program.Main(new[] { Program.STD_InstallCommand });

            var HTTPDefaultBrowserProgID = (string) WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath)
                .OpenSubKey(Program.AppName)
                .GetValue(Program.OrigHTTPProgIDValueName);
            var HTTPSDefaultBrowserProgID = (string) WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath)
                .OpenSubKey(Program.AppName)
                .GetValue(Program.OrigHTTPSProgIDValueName); ;

            Program.Main(new[] { Program.STD_DeleteCommand });

            DeleteTest(HTTPDefaultBrowserProgID, HTTPSDefaultBrowserProgID);
        }

        [Test]
        public void DeleteTest2(){
            var inpDefaultBrowserName = BrowserServ.FindBrowsers("")[1].Name;

            Program.Main(new[] { Program.STD_InstallCommand });
            Program.Main(new[] { Program.STD_DeleteCommand, inpDefaultBrowserName, inpDefaultBrowserName });

            DeleteTest(inpDefaultBrowserName, inpDefaultBrowserName);
        }

        [TearDown]
        public void Teardown(){
            var HTTPProgIDKey = WorkWithReg.GetKey(BrowserServ.HTTPDefaultBrowserKeyPath);
            HTTPProgIDKey.SetValue("ProgID", OrigHTTPDefaultBrowserProgID);
            HTTPProgIDKey.Close();

            var HTTPSProgIDKey = WorkWithReg.GetKey(BrowserServ.HTTPDefaultBrowserKeyPath);
            HTTPSProgIDKey.SetValue("ProgID", OrigHTTPSDefaultBrowserProgID);
            HTTPSProgIDKey.Close();

            var BrowsersKey = WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath);
            if(BrowsersKey.OpenSubKey(Program.AppName) != null) WorkWithReg.DeleteKey(BrowsersKey, Program.AppName);
            BrowsersKey.Close();

            var ProgIDsKey = WorkWithReg.GetKey(WorkWithReg.ProgIDsPath);
            if(ProgIDsKey.OpenSubKey(Program.AppProgID) != null) WorkWithReg.DeleteKey(ProgIDsKey, Program.AppProgID);
            ProgIDsKey.Close();
        }
    }
}
