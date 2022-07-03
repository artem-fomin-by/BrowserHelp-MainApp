// using System.IO;
// using System.Text.Json;
// using NUnit.Framework;
// using MainApp;
// using Logic;
// using NUnit.Framework.Interfaces;
//
// namespace MainProgramTests;
//
// [TestFixture]
// public class CongigurationFileTests{
//     private static bool HasPreviousTestPassed = true;
//
//     private const string ConfigurationJsonStringExample = @"{
//   ""Browsers"": [
//     {
//       ""Name"": ""Firefox-308046B0AF4A39CB"",
//       ""LaunchCommand"": ""\u0022C:\\Program Files\\Mozilla Firefox\\firefox.exe\u0022"",
//       ""LaunchCommandArgs"": """"
//     },
//     {
//       ""Name"": ""Google Chrome"",
//       ""LaunchCommand"": ""\u0022C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe\u0022"",
//       ""LaunchCommandArgs"": """"
//     },
//     {
//       ""Name"": ""Microsoft Edge"",
//       ""LaunchCommand"": ""\u0022C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe\u0022"",
//       ""LaunchCommandArgs"": """"
//     }
//   ]
// }";
//
//     [SetUp]
//     public void SetUp(){
//         Assume.That(HasPreviousTestPassed, Is.True);
//     }
//
//     [TearDown]
//     public void TearDown(){
//         if(HasPreviousTestPassed){
//             HasPreviousTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
//         }
//         else{
//             if(TestContext.CurrentContext.Test.Name == nameof(CreateConfigurationFileTest)){
//                 if(File.Exists("Configuration.json")) File.Delete("Configuration.json");
//             }
//         }
//     }
//
//     [Test, Order(1)]
//     public void CreateConfigurationFileTest(){
//         if(File.Exists("Configuration.json")) File.Delete("Configuration.json");
//
//         Program.CreateConfigurationFile();
//         Assert.True(File.Exists("Configuration.json"));
//
//         var configurationJsonString = File.ReadAllText("Configuration.json");
//
//         Assert.AreEqual(ConfigurationJsonStringExample, configurationJsonString);
//     }
//
//     [Test, Order(2)]
//     public void DeserializeConfigurationFileTest(){
//         Assert.True(Program.TryDeserializeConfigurationFile(out var deserializedBrowsers));
//         var foundBrowsers = Browser.FindBrowsers(Program.AppName);
//         Assert.AreEqual(foundBrowsers, deserializedBrowsers);
//     }
// }
