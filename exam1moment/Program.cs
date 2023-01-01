using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Xml.XPath;

namespace Exam1Moment
{
    internal class Program
    {
        private static string _email;
        private static string _password;
        public static void Main(string[] args)
        {
            DoSomeXPathCommand();
        }
        
        public static void DoSomeXPathCommand()
        {
            Console.WriteLine("Введите ссылку на тест:");
            var href = Console.ReadLine();
            
            var pathToFile = AppDomain.CurrentDomain.BaseDirectory + '\\';
            Console.WriteLine(pathToFile);
            var browserOptions = new ChromeOptions(); 
            browserOptions.AddArgument(@"user-data-dir=C:\Users\vasil\AppData\Local\Google\Chrome\User Data");
            //TODO: автоматизировать поиск директории браузера
            var driver = new ChromeDriver(pathToFile, browserOptions);

            driver.Navigate().GoToUrl(href);
            driver.FindElement(
                By.XPath("//*[@id=\"submitButton\"]")).Click();
            while (true)
            {
                SolveTest(driver);
                Thread.Sleep(400);
                driver.FindElement(By.XPath("//*[@class=\"float-right\"]")).Click();
                Thread.Sleep(100);
                driver.FindElement(By.XPath("//*[@class=\"btn btn-secondary\"]")).Click();
            }
        }

        private static void SolveTest(ChromeDriver driver)
        {
            while (true)
            {
                Thread.Sleep(2000);
                var questionCase = driver.FindElements(
                    By.XPath("//*[@class=\"ss-btn icon fa fa-magic fa-fw\"]"));
                Thread.Sleep(100);
                foreach (var item in questionCase)
                {
                    Console.WriteLine(item);
                    try
                    {
                        item.Click();
                        Thread.Sleep(100);
                        var suggest = driver.FindElement(By.XPath("//*[@id=\"page-mod-quiz-attempt\"]/ul/li[1]"));
                        suggest.Click();
                        Thread.Sleep(100);
                        var syncshareButton =
                            suggest.FindElement(By.XPath("ul/li/span"));
                        syncshareButton.Click();
                        Thread.Sleep(100);
                    }
                    catch
                    {
                        Console.WriteLine("Syncshare тут не смог");
                    }
                }

                Thread.Sleep(100);
                var nextQuestion = driver.FindElements(By.XPath("//*[@name=\"next\"]"));
                if (nextQuestion.Count > 0)
                    nextQuestion[0].Click();
                else
                {
                    var exitFromTestButton = driver.FindElements(By.XPath("//*[@class=\"btn btn-secondary\"]"));
                    exitFromTestButton.Last().Click();
                    Thread.Sleep(500);
                    var exitConfirm =
                        driver.FindElements(By.XPath("//*[@class=\"btn btn-primary\"]"));
                    if (exitConfirm.Count>0)
                        exitConfirm.Last().Click();
                    break;
                }
            }
        }
    }
}